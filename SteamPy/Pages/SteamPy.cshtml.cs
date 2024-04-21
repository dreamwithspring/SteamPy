using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Snowflake;
using steamPy.BaseModels;
using steamPy.Helpers;
using steamPy.Helpers.Extensions;
using steamPy.Models;
using steamPy.Models.VIewModels;
using steamPy.Services;
using SteamPy.SDK.Models.Domain;

namespace steamPy.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public class SteamPyViewModel : PageModel
    {
        private readonly ISteamPyServices _steamPyServices;
        private readonly IMemoryCache _cache;
        private readonly ILogger<SteamPyViewModel> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private long? UserId
        {
            get
            {
                var userId = "";
                _contextAccessor.HttpContext?.Request.Cookies.TryGetValue("userId", out userId);
                if (long.TryParse(userId,out long id))
                {
                    return id;
                }
                else
                {
                    return null;
                }
                
            }
        }

        #region CacheKey

        #endregion

        /// <summary>
        /// 游戏价格的展示
        /// </summary>
        public readonly GamePriceInfoHistory GamePriceInfo;
        public readonly UserWatch UserWatch;

        public SteamPyViewModel(ISteamPyServices steamPyServices, IMemoryCache cache, ILogger<SteamPyViewModel> logger, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _steamPyServices = steamPyServices;
            _cache = cache;
            _logger = logger;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public void OnGet()
        {
        }
        /// <summary>
        /// 获取游戏价格
        /// </summary>
        /// <param name="flag">是否必定重新获取数据并刷新缓存</param>
        /// <returns></returns>
        private async Task<List<GamePriceInfoHistory>> GetGamePriceInfosWithCache(bool flag = false)
        {
            List<GamePriceInfoHistory> gameList = new();
            if (!_cache.TryGetValue(nameof(CacheKey.Ch_GameList), out gameList) || flag)
            {
                _cache.Set(nameof(CacheKey.Ch_GameList), gameList);
            }
            return gameList;
        }

        public async Task<IActionResult> OnGetInitGameList()
        {
            return new OkResult();
        }

        public async Task<IActionResult> OnGetCheckUser()
        {
            if (UserId == null)
            {
                return new JsonResult(new { Successed = false });
            }
            var result = await _steamPyServices.CheckUserAsync(UserId.Value);
            return new JsonResult(new { Successed = result });
        }

        public async Task<IActionResult> OnPostGetGameListByPage(int current, int size, string Search)
        {
            var gameList = new List<GamePriceInfo>();
            var games = gameList
                .WhereIf(!string.IsNullOrEmpty(Search), x => x.GameNameCN.Contains(Search)
                || x.GameName.Contains(Search)).ToList();
            if (games.Count < (current - 1) * size)
            {
                current = games.Count / size + games.Count % size > 0 ? 1 : 0;
            }
            var result = games?.Skip((current - 1) * size).Take(size).ToList();
            return new JsonResult(new { result, current, size, total = games.Count });
        }

        public async Task<IActionResult> OnPostGetUserGameListByPage(int current, int size, string search)
        {
            if (UserId == null)
            {
                return new JsonResult(new { result = new List<GamePriceInfoHistory>(), current, size, total = 0 });
            }
            var gameList = await _steamPyServices.RefreceUserGameInfo(UserId.GetValueOrDefault(), search, current,size);

            return new JsonResult(gameList);
        }


        public async Task<IActionResult> OnPostFollowGameClick(UserWatch watch)
        {
            if (UserId == null) { return new JsonResult(new { Successed = false }); }
            watch.UserId = UserId.GetValueOrDefault();
            await _steamPyServices.InsertOrUpdateGameToUser(watch);
            return new JsonResult(new { Successed = true });
        }

        public async Task<IActionResult> OnPostFollowUserGameRemoveClick(string gameId)
        {
            if (UserId == null) { return new JsonResult(new { Successed = false }); }

            await _steamPyServices.RemoveGameToUser(gameId, UserId.GetValueOrDefault());
            return new JsonResult(new { Successed = true });
        }

        public async Task<IActionResult> OnPostGetPyGameListByPage(int current, int size, string gameName, string? sort)
        {
            PageModel<GamePriceInfoHistory> games;
            if (gameName.IsNullOrEmpty())
            {
                games = await _steamPyServices.GetPyGamePriceInfoAndSaveAsync(current, size, sort) ?? new();
            }
            else
            {
                games = await _steamPyServices.GetPySpecifiedGamePriceAndSaveAsync(gameName, current, size, sort) ?? new();
            }
            var allGames = await _steamPyServices.GetDbAllGamePriceInfoMaxAsync();
            _cache.Set(nameof(CacheKey.Ch_GameList), games);
            return new JsonResult(new {result = games.Result, total = games.Total,size = games.Size,current = games.Current+1 });
        }

        public async Task<IActionResult> OnPostLogin(string userName, string password)
        {
            var result = await _steamPyServices.ValidatePasswordAsync(userName, password);
            if (!result)
            {
                return new JsonResult(new { Successed = false});

            }
            var user = await _steamPyServices.GetUserByNameAsync(userName);

            return new JsonResult(new { Successed = result, UserId =  user?.Id.ToString() ?? ""});

        }

        public async Task<IActionResult> OnPostCreate(string userName, string password,string email)
        {
            var user = new UserInfo
            {
                Id = new IdWorker(1, 2).NextId(),
                Email = email,
                Password = password,
                UserName = userName,
            };
            var result = await _steamPyServices.CreateUserAsync(user);
            return new JsonResult(new { Successed = result > 0 });
        }
    }
}
