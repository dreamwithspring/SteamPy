using SteamPy.SDK.Models.Base;
using SteamPy.SDK.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamPy.SDK.Models.Request
{
    public class GetSpecifiedGame : PY_BaseRequest<Contents<GameInfo>>
    {
        public string gameName { get; set; }
        public string gameUrl { get; set; }

        public GetSpecifiedGame()
        {
            
        }

        public override GetSpecifiedGame Build()
        {
            var url = new Uri( $"https://steampy.com/xboot/steamGame/keyByName?pageNumber={pageNumber}&pageSize={pageSize}&sort=keyTx&order={order}&startDate={startDate}&endDate={endDate}&gameName={gameName.Replace(" ","+")}&gameUrl={gameUrl}");
            Url = url.AbsoluteUri;

            return this;
        }
    }
}
