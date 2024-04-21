using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamPy.SDK.Models.Domain
{
    /// <summary>
    /// 外面搜索的
    /// </summary>
    public class GameInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string? id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? createBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? updateBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? updateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? delFlag { get; set; }
        /// <summary>
        /// 完蛋！我被美女包围了！
        /// </summary>
        public string? gameName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? gameNameCn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? oriPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? gamePrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? hisPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? hisFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? gameAvaLib { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? gameAva { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? iosAva { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? gameUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? gamePath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? freeSub { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? snr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? bundleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? gameStatus { get; set; }
        /// <summary>
        /// 完蛋我被美女包围了
        /// </summary>
        public string? gameKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? gameTx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? sortOrder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? keySortOrder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? discount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? startFree { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? endFree { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? keySales { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? keyTx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? keyPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? keyTxAmt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? keyAveAmt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? keyDiscount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? appId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? rating { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? publisher { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? promoFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? basicFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? syncUs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? crossArea { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? promoEnd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SteamApp steamApp { get; set; }
    }
}
