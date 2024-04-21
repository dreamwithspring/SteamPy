using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steamPy.Models
{
    /// <summary>
    /// 存最低价格
    /// </summary>
    public class GamePriceInfo
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

    }
}
