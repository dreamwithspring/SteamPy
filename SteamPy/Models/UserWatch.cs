using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steamPy.Models
{
    public class UserWatch
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [DisplayName("用户Id")]
        public long UserId { get; set; }

        [DisplayName("游戏Id")]
        public string GameId { get; set; }

        [DisplayName("游戏名")]
        public string? GameName { get; set; }

        [DisplayName("游戏名")]
        public string? GameNameCN { get; set; }

        [DisplayName("steam游戏链接")]
        public string? SteamUrl { get; set; }

        [NotMapped]
        [DisplayName("当前价格")]
        public decimal? NowLowPrice { get; set; }

        [DisplayName("发送邮件的价格")]
        public decimal? SendMailPrice { get; set; }
    }
}
