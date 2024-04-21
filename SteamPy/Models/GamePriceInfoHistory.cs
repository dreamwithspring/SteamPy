using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace steamPy.Models
{
    public class GamePriceInfoHistory 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [DisplayName("游戏Id")]
        public string? GameId { get; set; }


        [DisplayName("steam游戏名")]
        public string? GameName { get; set; }

        [DisplayName("steam游戏名")]
        public string? GameNameCN { get; set; }


        [DisplayName("steam链接")]
        public string? SteamUrl { get; set; }

        [DisplayName("steam价格")]
        public decimal? SteamPrice { get; set; }

        [DisplayName("steam价格")]
        public decimal? SteamPriceNow { get; set; }

        [DisplayName("steam最低")]
        public decimal? SteamPriceHistory { get; set; }


        /// <summary>
        /// 总最低
        /// </summary>
        [DisplayName("总最低")]
        public decimal? AllLowPrice { get; set; }

        [DisplayName("价格日期")]
        public DateTime PriceDate { get; set; } = DateTime.Now;

        [NotMapped]
        [DisplayName("价格日期")]
        public string PriceDateInfo => PriceDate.ToString("yyyy年MM月dd日");

        /// <summary>
        /// 当日最低
        /// </summary>
        [DisplayName("当日最低")]
        public decimal? DayLowPrice { get; set; }

        /// <summary>
        /// 最低前十的平均
        /// </summary>
        [DisplayName("平均价格")]
        public decimal? AvgPrice { get; set; }

    }
}
