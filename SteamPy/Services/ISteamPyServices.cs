using steamPy.BaseModels;
using steamPy.Models;
using SteamPy.SDK.Models.Domain;

namespace steamPy.Services
{
    public interface ISteamPyServices
    {
        Task<int> CreateOrUpdateDbGamePriceInfoAsync(List<GamePriceInfoHistory> infos);
        Task<int> CreateOrUpdateDbGamePriceInfoAsync(GamePriceInfoHistory info);
        Task<List<GamePriceInfoHistory>> GetDbAllGamePriceInfoAsync();
        Task<List<GamePriceInfoHistory>> GetDbAllGamePriceInfoMaxAsync();
        Task<List<SaleInfo>> GetPyAllSaleListAsync(string gameId);
        Task<GameInfo?> GetPySpecifiedGameInfoAsync(GamePriceInfo info);
        Task RefreshGameInfoAsync();
        Task<List<UserWatch>> GetUserWatchesAsync(long userId);
        Task<PageModel<UserWatch>> RefreceUserGameInfo(long userId,string search, int current = 1, int size = 20);
        Task InsertOrUpdateGameToUser(UserWatch watch);
        Task RemoveGameToUser(string gameId, long userId);
        Task<PageModel<GamePriceInfoHistory>> GetPyGamePriceInfoAsync(int current, int size, string? sort);
        Task<PageModel<GamePriceInfoHistory>> GetPyGamePriceInfoAndSaveAsync(int current, int size, string? sort);
        Task<PageModel<GamePriceInfoHistory>> GetPySpecifiedGamePricePageAsync(string gameName, int current = 1, int size = 20, string? sort = "createTime");
        Task<PageModel<GamePriceInfoHistory>> GetPySpecifiedGamePriceAndSaveAsync(string gameName, int current = 1, int size = 20, string? sort = "createTime");
        Task<bool> ValidatePasswordAsync(string name, string password);
        Task<int> CreateUserAsync(UserInfo userInfo);
        Task<UserInfo?> GetUserByNameAsync(string name);
        Task<GameInfo?> GetPySpecifiedGameInfoAsync(string gameName, string gameId);
        Task<GamePriceInfoHistory?> GetPySpecifiedGamePriceInfoAsync(string gameName, string gameId);
        Task RefreshAndSendMail();
        Task<bool> CheckUserAsync(long userId);
    }
}
