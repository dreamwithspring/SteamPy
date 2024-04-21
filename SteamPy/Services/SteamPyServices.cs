using System.Diagnostics;
using System.Net.Cache;
using System.Net;
using System.Text;
using SteamPy.SDK.Models.Domain;
using SteamPy.SDK.Models.Request;
using steamPy.Db;
using Microsoft.EntityFrameworkCore;
using steamPy.Models;
using steamPy.Models.VIewModels;
using Microsoft.EntityFrameworkCore.Internal;
using AutoMapper;
using SQLitePCL;
using steamPy.Helpers.Extensions;
using System.Collections.Generic;
using System.IO.Pipelines;
using steamPy.Helpers;
using SteamPy.SDK.Models.Base;
using Snowflake;
using steamPy.BaseModels;
using steamPy.Helpers.IHelpers;

namespace steamPy.Services
{
    public class SteamPyServices : ISteamPyServices
    {
        private readonly SteamPyDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<SteamPyServices> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;

        public SteamPyServices(SteamPyDbContext db, ILogger<SteamPyServices> logger, IMapper mapper, IConfiguration configuration, IMailHelper mailHelper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _mailHelper = mailHelper;
        }

        #region 从Py取数据
        public async Task<PageModel<GamePriceInfoHistory>> GetPyGamePriceInfoAsync(int current, int size, string? sort)
        {
            GetAllGameList request = new GetAllGameList()
            {
                pageSize = size,
                pageNumber = current,
                sortEnum = sort.ToEnum(SortEnum.createTime),
                Accesstoken = _configuration["SteamPy:Accesstoken"]
            };

            var resp = await request.Build().ExecuteAsync();

            var result = new PageModel<GamePriceInfoHistory>();
            if (resp.success)
            {
                result = _mapper.Map<PageModel<GamePriceInfoHistory>>(resp.result);
            }
            return result;
        }

        public async Task<PageModel<GamePriceInfoHistory>> GetPySpecifiedGamePricePageAsync(string gameName, int current = 1, int size = 20, string? sort = "createTime")
        {
            GetSpecifiedGame request = new GetSpecifiedGame()
            {
                pageNumber = current,
                pageSize = size,
                sortEnum = sort.ToEnum(SortEnum.createTime),
                gameName = gameName,
                Accesstoken = _configuration["SteamPy:Accesstoken"]
            };

            var resp = await request.Build().ExecuteAsync();
            var result = new PageModel<GamePriceInfoHistory>();
            if (resp.success)
            {
                result = _mapper.Map<PageModel<GamePriceInfoHistory>>(resp.result);
            }
            return result;
        }


        public async Task<GameInfo?> GetPySpecifiedGameInfoAsync(GamePriceInfo info)
        {
            GetSpecifiedGame request = new GetSpecifiedGame()
            {
                gameName = info.GameName,
                Accesstoken = _configuration["SteamPy:Accesstoken"]
            };

            var resp = await request.Build().ExecuteAsync();
            var result = new List<GameInfo>();
            if (resp.success)
            {
                result = resp.result.Content;
            }
            return result.FirstOrDefault(x => x.id == info.GameId) ?? new GameInfo();
        }

        public async Task<GameInfo?> GetPySpecifiedGameInfoAsync(string gameName, string gameId)
        {
            return await GetPySpecifiedGameInfoAsync(new GamePriceInfo { GameName = gameName, GameId = gameId });
        }

        public async Task<GamePriceInfoHistory?> GetPySpecifiedGamePriceInfoAsync(string gameName, string gameId)
        {
            var game = await GetPySpecifiedGameInfoAsync(new GamePriceInfo { GameName = gameName, GameId = gameId });
            return _mapper.Map<GamePriceInfoHistory>(game);
        }



        public async Task<List<SaleInfo>> GetPyAllSaleListAsync(string gameId)
        {
            GetAllSaleList request = new GetAllSaleList()
            {
                gameId = gameId,
                Accesstoken = _configuration["SteamPy:Accesstoken"]
            };
            var resp = await request.Build().ExecuteAsync();
            var result = new List<SaleInfo>();
            if (resp.success)
            {
                result = resp.result.Content;
            }
            return result;
        }


        #endregion

        public async Task<bool> CheckUserAsync(long userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return user != null;
        }


        public async Task<PageModel<GamePriceInfoHistory>> GetPyGamePriceInfoAndSaveAsync(int current, int size, string? sort)
        {
            var games = await GetPyGamePriceInfoAsync(current, size, sort);
            var pyGames = games.Result.ToList();

            var gameInfos = await _db.GamePriceInfos.ToListAsync();
            int a = new Random().Next(1,100);
            var idworker = new IdWorker(1, 2, a);
            foreach (var item in pyGames)
            {
                var game = gameInfos.FirstOrDefault(x => x.GameId == item.GameId);
                if (game == null)
                {
                    item.Id = idworker.NextId();
                    await _db.GamePriceInfos.AddAsync(_mapper.Map<GamePriceInfo>(item));
                }
                else if (game.AllLowPrice > item.AllLowPrice)
                {
                    game.AllLowPrice = item.AllLowPrice;
                }
                await _db.GamePriceInfoHistories.AddAsync(_mapper.Map<GamePriceInfoHistory>(item));
            }
            await _db.SaveChangesAsync();
            return games;
        }




        public async Task<PageModel<GamePriceInfoHistory>> GetPySpecifiedGamePriceAndSaveAsync(string gameName, int current, int size, string? sort)
        {
            var games = await GetPySpecifiedGamePricePageAsync(gameName);

            var idWorder = new IdWorker(1, 2);
            var gameHistories = games.Result.Select(x =>
            {
                x.Id = idWorder.NextId();
                return x;
            }).ToList();

            var gameInfos = await _db.GamePriceInfos.ToListAsync();
            foreach (var item in gameHistories)
            {
                var game = gameInfos.FirstOrDefault(x => x.GameId == item.GameId);
                if (game == null)
                {
                    await _db.GamePriceInfos.AddAsync(_mapper.Map<GamePriceInfo>(item));
                }
                else if (game.AllLowPrice > item.AllLowPrice)
                {
                    game.AllLowPrice = item.AllLowPrice;
                }
            }
            await _db.GamePriceInfoHistories.AddRangeAsync(gameHistories);
            await _db.SaveChangesAsync();
            return games;
        }


        public async Task<List<GamePriceInfoHistory>> GetDbAllGamePriceInfoAsync()
        {
            var list = await _db.GamePriceInfoHistories.OrderBy(x => x.GameName).ToListAsync();
            return list;
        }

        public async Task<List<GamePriceInfoHistory>> GetDbAllGamePriceInfoMaxAsync()
        {
            var infos = await _db.GamePriceInfoHistories.AsNoTracking()
                .ToListAsync();
            var list = infos.GroupBy(x => x.GameId).Select(x => new
            {
                GameId = x.Key,
                Price = x.Min(y => y.DayLowPrice)
            });
            var result = list.Select(x => {
                return _db.GamePriceInfoHistories.AsNoTracking()
              .FirstOrDefault(y => y.GameId == x.GameId && y.DayLowPrice == x.Price);
            }).ToList() ?? new List<GamePriceInfoHistory?>();
            return result;    
        }


        public async Task<int> CreateOrUpdateDbGamePriceInfoAsync(GamePriceInfoHistory info)
        {
            if (await _db.GamePriceInfoHistories.AnyAsync(x => x.GameId == info.GameId && x.PriceDate == info.PriceDate))
            {

                var old = await _db.GamePriceInfoHistories.FirstAsync(x => x.GameId == info.GameId && x.PriceDate == info.PriceDate);
                old.AllLowPrice = info.AllLowPrice;
                old.DayLowPrice = info.DayLowPrice;
                old.AvgPrice = info.AvgPrice;
            }
            else
            {
                await _db.GamePriceInfoHistories.AddAsync(info);
            }
            var res = await _db.SaveChangesAsync();
            return res;
        }

        public async Task<int> CreateOrUpdateDbGamePriceInfoAsync(List<GamePriceInfoHistory> infos)
        {
            foreach (var info in infos)
            {
                if (await _db.GamePriceInfoHistories.AnyAsync(x => x.GameId == info.GameId && x.PriceDate == info.PriceDate))
                {

                    var old = await _db.GamePriceInfoHistories.FirstAsync(x => x.GameId == info.GameId && x.PriceDate == info.PriceDate);
                    old.AllLowPrice = info.AllLowPrice;
                    old.DayLowPrice = info.DayLowPrice;
                    old.AvgPrice = info.AvgPrice;
                }
                else
                {
                    await _db.GamePriceInfoHistories.AddAsync(info);
                }
            }
            var res = await _db.SaveChangesAsync();
            return res;
        }

        public async Task<List<UserWatch>> GetUserWatchesAsync(long userId)
        {
            var result = await _db.UserWatchList.Where(x => x.UserId == userId).ToListAsync();
            return result ?? new List<UserWatch>();
        }
        
        /// <summary>
        /// 刷新所有游戏的列表
        /// </summary>
        /// <returns></returns>
        public async Task RefreshGameInfoAsync()
        {
            var prices = await _db.GamePriceInfos.ToListAsync();
            foreach (var price in prices)
            {
                var nowGame = await GetPySpecifiedGamePriceInfoAsync(price.GameName, price.GameId);
                if (nowGame == null)
                {
                    continue;
                }
                nowGame.Id = new IdWorker(1, 2).NextId();
                await _db.GamePriceInfoHistories.AddAsync(_mapper.Map<GamePriceInfoHistory>(nowGame));
                if (price.AllLowPrice > nowGame.AllLowPrice)
                {
                    price.AllLowPrice = nowGame.AllLowPrice;
                }
            }
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// 刷新用户的列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<PageModel<UserWatch>> RefreceUserGameInfo(long userId,string search, int current = 1, int size = 20)
        {
            var allWatches = await _db.UserWatchList.AsNoTracking()
                .Where(x => x.UserId == userId).ToListAsync() ?? new List<UserWatch>();
            var watches = allWatches
                .WhereIf(!string.IsNullOrEmpty(search),x => (x.GameName?.Contains(search) ?? false) || (x.GameNameCN?.Contains(search) ?? false)).ToList();
            var total = watches.Count();
            if (size < 5)
            {
                size = 20;
            }
            if (current < 1)
            {
                current = 1;
            }
            if (current * size > total)
            {
                current = total / size + 1;
            }

            var userWatches = watches.OrderBy(x => x.GameId).Skip((current-1) * size).Take(size).ToList();
            var prices = await _db.GamePriceInfos.AsNoTracking().ToListAsync();
            List<GamePriceInfo> list = new List<GamePriceInfo>();
            foreach (var user in userWatches)
            {
                if ((user.GameId?.IsNullOrEmpty() ?? true) || (user.GameName?.IsNullOrEmpty() ?? true))
                {
                    continue;
                }

                var nowGame = _mapper.Map<GamePriceInfoHistory>( prices.FirstOrDefault(x => x.GameId == user.GameId));

                if (nowGame == null)
                {
                    nowGame = await GetPySpecifiedGamePriceInfoAsync(user.GameNameCN ?? user.GameName, user.GameId) ;
                    if (nowGame == null || (nowGame.GameId?.IsNullOrEmpty() ?? true))
                    {
                        continue;
                    }

                }

                var sales = await GetPyAllSaleListAsync(user.GameId);
                if (sales.IsNullOrEmpty())
                {
                    continue;
                }

                nowGame.AllLowPrice = Math.Min(sales.Min(x => x.keyPrice), nowGame.AllLowPrice ?? 9999m);
                nowGame.DayLowPrice = sales.Min(x => x.keyPrice);
                nowGame.Id = new IdWorker(1, 2).NextId();
                user.NowLowPrice = nowGame.AllLowPrice;


                list.Add(_mapper.Map<GamePriceInfo>(nowGame));
                await _db.GamePriceInfoHistories.AddAsync(nowGame);
                var price = prices.FirstOrDefault(x => x.GameId == nowGame.GameId);
                if (price == null)
                {
                    await _db.GamePriceInfos.AddAsync(_mapper.Map<GamePriceInfo>(nowGame));
                }
                else if (price.AllLowPrice > nowGame.AllLowPrice)
                {
                    await _db.GamePriceInfos.Where(x => x.GameId == user.GameId).ForEachAsync(x =>
                    {
                        x.AllLowPrice = nowGame.AllLowPrice;
                    });
                }
            }
            await _db.SaveChangesAsync();

            return new PageModel<UserWatch>
            {
                Result = userWatches,
                Size = size,
                Current = current,
                Total = total
            };
        }

        public async Task InsertOrUpdateGameToUser(UserWatch watch)
        {

            var userWatch = await _db.UserWatchList.FirstOrDefaultAsync(x => x.UserId == watch.UserId && x.GameId == watch.GameId);
            if (userWatch == null)
            {
                var game = await _db.GamePriceInfos.AsNoTracking().FirstOrDefaultAsync(x => x.GameId == watch.GameId);
                if (game != null)
                {
                    watch.SteamUrl = game.SteamUrl;
                }
                watch.Id = new IdWorker(1, 2).NextId();
                await _db.UserWatchList.AddAsync(watch);
            }
            else
            {
                userWatch.SendMailPrice = watch.SendMailPrice;
            }

            await _db.SaveChangesAsync();

        }

        public async Task RemoveGameToUser(string gameId, long userId)
        {
            var watch = await _db.UserWatchList.FirstOrDefaultAsync(x => x.GameId == gameId && x.UserId == userId);
            if (watch != null)
            {
                _db.UserWatchList.Remove(watch);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<UserInfo?> GetUserByNameAsync(string name)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserName == name);
            return user;
        }

        public async Task<int> CreateUserAsync(UserInfo userInfo)
        {
            if (await _db.Users.AsNoTracking().AnyAsync(x => x.UserName == userInfo.UserName))
            {
                throw new InvalidOperationException($"用户名{userInfo.UserName}已使用");
            }
            else
            {
                Random random = new Random();
                var length = random.Next(6, 10);
                var a = length;
                List<byte> bytes = new List<byte>();
                for (int i = 0; i < length; i++)
                {
                    random = new Random(a);
                    a = random.Next(48, 126);
                    bytes.Add((byte)a);
                }
                var salt = "nb" + Encoding.ASCII.GetString(bytes.ToArray());
                var saltEnc = EncryptHelper.DESEncrypt(salt);
                userInfo.Salt = saltEnc;
                userInfo.Password = EncryptHelper.MD5Encrypt(userInfo.Password ?? "", salt);
                await _db.Users.AddAsync(userInfo);
                var result = await _db.SaveChangesAsync();
                return result;
            }
        }

        public async Task<bool> ValidatePasswordAsync(string name,string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserName == name);
            if (user == null )
            {
                return false;
            }
            var salt = EncryptHelper.DESDecrypt(user.Salt);
            return EncryptHelper.MD5Encrypt(password, salt) == user.Password;
        }


        public async Task RefreshAndSendMail()
        {
            var users = await _db.Users.AsNoTracking().ToListAsync();

            var userWatches = await _db.UserWatchList.AsNoTracking().ToListAsync() ?? new List<UserWatch>();
            var messages = userWatches.Select(x => x.UserId)
                ?.Distinct()
                ?.ToDictionary(x => x, y => "") ?? new Dictionary<long, string>();
            var games = _mapper.Map<List<GamePriceInfo>>(userWatches) ?? new List<GamePriceInfo>();
            var prices = await _db.GamePriceInfos.ToListAsync();
            foreach (var game in games)
            {
                if ((game.GameId?.IsNullOrEmpty() ?? true) || (game.GameName?.IsNullOrEmpty() ?? true))
                {
                    continue;
                }

                var nowGame = _mapper.Map<GamePriceInfoHistory>(prices.FirstOrDefault(x => x.GameId == game.GameId));

                if (nowGame == null)
                {
                    nowGame = await GetPySpecifiedGamePriceInfoAsync(game.GameNameCN ?? game.GameName, game.GameId);
                    if (nowGame == null || (nowGame.GameId?.IsNullOrEmpty() ?? true))
                    {
                        continue;
                    }

                }

                var sales = await GetPyAllSaleListAsync(game.GameId);
                if (sales.IsNullOrEmpty())
                {
                    continue;
                }
                nowGame.AllLowPrice = Math.Min(sales.Min(x => x.keyPrice), nowGame.AllLowPrice ?? 9999m); 

                var watchUser = userWatches.Where(x => x.GameId == game.GameId).ToList() ?? new List<UserWatch>();
                foreach (var watch in watchUser)
                {
                    if (watch.SendMailPrice > nowGame.AllLowPrice)
                    {
                        await _db.UserWatchList.Where(x => x.UserId == watch.UserId && x.GameId == watch.GameId).ForEachAsync(x =>
                        {
                            x.SendMailPrice = nowGame.AllLowPrice - 0.1m;
                        });
                        messages[watch.UserId] += $"\r\n{nowGame.GameName}的价格{nowGame.AllLowPrice}低于{watch.SendMailPrice}";
                    }
                }

                nowGame.Id = new IdWorker(1, 2).NextId();
                await _db.GamePriceInfoHistories.AddAsync(_mapper.Map<GamePriceInfoHistory>(nowGame));
                var price = prices.FirstOrDefault(x => x.GameId == nowGame.GameId);
                if (price == null)
                {
                    await _db.GamePriceInfos.AddAsync(_mapper.Map<GamePriceInfo>(nowGame));
                }
                else if (price.AllLowPrice > nowGame.AllLowPrice)
                {
                    await _db.GamePriceInfos.Where(x => x.GameId == game.GameId).ForEachAsync(x =>
                    {
                        x.AllLowPrice = nowGame.AllLowPrice;
                    });
                }
            }

            var sendMessages = messages.Where(x => !x.Value.IsNullOrEmpty()).ToList();
            if (!sendMessages.IsNullOrEmpty())
            {
                sendMessages.Select(x => new MailInfo
                {
                    Message = x.Value,
                    EmailAddress = users?.FirstOrDefault(y => y.Id == x.Key)?.Email ?? "",
                    Title = "【SteamPY关注名单】"

                }).Where(x => !x.EmailAddress.IsNullOrEmpty()).ToList().ForEach(x =>
                {
                    _mailHelper.SendMail(x);
                });

            }
            await _db.SaveChangesAsync();
        }
    }
}
