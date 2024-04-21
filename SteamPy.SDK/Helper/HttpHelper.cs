using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamPy.SDK.Helper
{
    public class HttpHelper
    {
        private static Lazy<HttpClient> _lazy = new Lazy<HttpClient>(() => {
            return new HttpClient();
        }, LazyThreadSafetyMode.ExecutionAndPublication);

        public HttpClient MyHttplClient
        {
            get
            {
                return _lazy.Value;
            }
        }
    }
}
