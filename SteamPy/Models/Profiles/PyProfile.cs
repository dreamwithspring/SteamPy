using AutoMapper;
using Snowflake;
using steamPy.BaseModels;
using steamPy.Helpers.Extensions;
using steamPy.Models.VIewModels;
using SteamPy.SDK.Models.Domain;

namespace steamPy.Models.Profiles
{
    public class PyProfile : Profile
    {
        public PyProfile()
        {
            CreateMap<GamePriceInfoHistory, GamePriceInfo>();
            CreateMap<GamePriceInfo, GamePriceInfoHistory>();
            CreateMap<GameInfo, GamePriceInfo>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.GameName, opt => opt.MapFrom(src => src.steamApp.gameName))
                .ForMember(dest => dest.GameNameCN, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.gameNameCn) ? (src.steamApp.gameName.IsNullOrEmpty() ? src.gameName : src.steamApp.gameName) : src.gameNameCn))
                .ForMember(dest => dest.SteamUrl, opt => opt.MapFrom(src => src.gameUrl))
                .ForMember(dest => dest.SteamPrice, opt => opt.MapFrom(src => src.oriPrice))
                .ForMember(dest => dest.SteamPriceNow, opt => opt.MapFrom(src => src.gamePrice))
                .ForMember(dest => dest.SteamPriceHistory, opt => opt.MapFrom(src => src.hisPrice))
                .ForMember(dest => dest.AllLowPrice, opt => opt.MapFrom(src => Math.Min(src.keyTxAmt ?? 9999, src.keyPrice ?? 9999)));


            CreateMap<GameInfo, GamePriceInfoHistory>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.GameName, opt => opt.MapFrom(src => src.steamApp.gameName))
                .ForMember(dest => dest.GameNameCN, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.gameNameCn) ? (src.steamApp.gameName.IsNullOrEmpty() ? src.gameName : src.steamApp.gameName) : src.gameNameCn))
                .ForMember(dest => dest.SteamUrl, opt => opt.MapFrom(src => src.gameUrl))
                .ForMember(dest => dest.SteamPrice, opt => opt.MapFrom(src => src.oriPrice))
                .ForMember(dest => dest.SteamPriceNow, opt => opt.MapFrom(src => src.gamePrice))
                .ForMember(dest => dest.SteamPriceHistory, opt => opt.MapFrom(src => src.hisPrice))
                .ForMember(dest => dest.PriceDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.DayLowPrice, opt => opt.MapFrom(src => src.keyTxAmt))
                .ForMember(dest => dest.AllLowPrice, opt => opt.MapFrom(src => Math.Min(src.keyTxAmt ?? 9999,src.keyPrice ?? 9999)))
                .ForMember(dest => dest.AvgPrice, opt => opt.MapFrom(src => src.keyAveAmt));

            CreateMap<Contents<GameInfo>, PageModel<GamePriceInfo>>().ConvertUsing<PyConentsToPricesConverter>();
            CreateMap<Contents<GameInfo>, PageModel<GamePriceInfoHistory>>().ConvertUsing<PyConentsToPricesHistoryConverter>();
            CreateMap<UserWatch, GamePriceInfo>();

            CreateMap<GamePriceInfo, GamePriceInfoHistory>();
            CreateMap<GamePriceInfoHistory, GamePriceInfo>();
        }



        public class PyConentsToPricesConverter : ITypeConverter<Contents<GameInfo>, PageModel<GamePriceInfo>>
        {
            public PageModel<GamePriceInfo> Convert(Contents<GameInfo> source, PageModel<GamePriceInfo> destination, ResolutionContext context)
            {
                var result = new PageModel<GamePriceInfo>();
                result.Size = source.size;
                result.Current = source.number;
                result.Total = source.totalElements;

                IdWorker idWorker = new IdWorker(1, 2, new Random().Next(0, 127));
                var infos = source.Content?.Select(x => new GamePriceInfo
                {
                    Id = idWorker.NextId(),
                    GameId = x.id,
                    GameName = x.gameName,
                    GameNameCN = x.gameNameCn.IsNullOrEmpty() ? (x.steamApp.gameName.IsNullOrEmpty() ? x.gameName : x.steamApp.gameName) : x.gameNameCn,
                    SteamUrl = x.gameUrl,
                    SteamPrice = x.oriPrice,
                    SteamPriceNow = x.gamePrice,
                    SteamPriceHistory = x.hisPrice,
                    AllLowPrice = x.keyTxAmt
                })?.ToList();
                result.Result = infos ?? new List<GamePriceInfo>();
                return result;
            }
        }
        public class PyConentsToPricesHistoryConverter : ITypeConverter<Contents<GameInfo>, PageModel<GamePriceInfoHistory>>
        {
            public PageModel<GamePriceInfoHistory> Convert(Contents<GameInfo> source, PageModel<GamePriceInfoHistory> destination, ResolutionContext context)
            {
                var result = new PageModel<GamePriceInfoHistory>();
                result.Size = source.size;
                result.Current = source.number;
                result.Total = source.totalElements;

                IdWorker idWorker = new IdWorker(1, 2, new Random().Next(0, 127));
                var infos = source.Content?.Select(x => new GamePriceInfoHistory
                {
                    Id = idWorker.NextId(),
                    GameId = x.id,
                    GameName = x.gameName,
                    GameNameCN = x.gameNameCn.IsNullOrEmpty() ? (x.steamApp.gameName.IsNullOrEmpty() ? x.gameName : x.steamApp.gameName) : x.gameNameCn,
                    SteamUrl = x.gameUrl,
                    SteamPrice = x.oriPrice,
                    SteamPriceNow = x.gamePrice,
                    SteamPriceHistory = x.hisPrice,
                    AllLowPrice = Math.Min(x.keyTxAmt ?? 9999, x.keyPrice ?? 9999),
                    PriceDate = DateTime.Now,
                    DayLowPrice = x.keyTxAmt,
                    AvgPrice = x.keyAveAmt

            })?.ToList();
                result.Result = infos ?? new List<GamePriceInfoHistory>();
                return result;
            }
        }
    }
}
