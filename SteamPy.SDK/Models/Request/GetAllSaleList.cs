using SteamPy.SDK.Models.Base;
using SteamPy.SDK.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamPy.SDK.Models.Request
{
    public class GetAllSaleList : PY_BaseRequest<Contents<SaleInfo>>
    {
        public string gameId { get; set; }

        public GetAllSaleList() { }

        public override PY_BaseRequest<Contents<SaleInfo>> Build()
        {
            Url = $"https://steampy.com/xboot/steamKeySale/listSale?pageNumber={pageNumber}&pageSize={pageSize}&sort={sort}&order={order}&startDate={startDate}&endDate={endDate}&gameId={gameId}";
            return base.Build();
        }
    }
}
