using SteamPy.SDK.Models.Base;
using SteamPy.SDK.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamPy.SDK.Models.Request
{
    public class GetAllGameList : PY_BaseRequest<Contents<GameInfo>>
    {
        public GetAllGameList() 
        { 

        }


        public override GetAllGameList Build() 
        {
            Url = $"https://steampy.com/xboot/steamGame/keyHot?pageNumber={pageNumber}&pageSize={pageSize}&sort={sort}&order={order}&startDate={startDate}&endDate={endDate}";

            return this; 
        }
    }
}
