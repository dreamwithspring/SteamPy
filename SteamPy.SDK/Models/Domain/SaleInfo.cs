using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamPy.SDK.Models.Domain
{
    /// <summary>
    /// 进去看详细的
    /// </summary>
    public class SaleInfo
    {
        public string steamAva { get; set; }

        [Key]
        public string steamId { get; set; }

        public decimal keyPrice { get; set; }

        public string country { get; set; }

        public decimal sold { get; set; }

        public string saleId { get; set; }

        public string ccy { get; set; }

        public decimal discount { get; set; }

        public decimal stock { get; set; }

        public string steamName { get; set; }



    }
}
