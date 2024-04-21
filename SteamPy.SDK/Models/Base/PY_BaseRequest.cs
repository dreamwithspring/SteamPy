using Newtonsoft.Json;
using SteamPy.SDK.Helper;
using SteamPy.SDK.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SteamPy.SDK.Models.Base
{
    public class PY_BaseRequest<T> where T : class
    {
        /// <summary>
        /// 链接
        /// </summary>
        public virtual string Url { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int pageNumber { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int pageSize { get; set; } = 30;

        /// <summary>
        /// 排序方式
        /// </summary>
        public string sort => sortEnum.ToString();

        /// <summary>
        /// 排序方式
        /// </summary>
        public SortEnum sortEnum { get; set; } = SortEnum.createTime;


        public OrderEnum OrderEnum { get; set; } = OrderEnum.desc;
        /// <summary>
        /// 顺序倒序
        /// </summary>
        public string order => OrderEnum.ToString();

        /// <summary>
        /// 开始日期
        /// </summary>
        public string startDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string endDate { get; set; }

        public readonly Dictionary<string, string> Cookies = new Dictionary<string, string>();
        public readonly Dictionary<string, string> Headers = new Dictionary<string, string>();
        public string Accesstoken { get; set; } 


        public virtual PY_BaseRequest<T> Build()
        {
            return this;
        }

        public async Task<PY_BaseResponse<T>> ExecuteAsync()
        {

            HttpHelper httpHelper = new();
            try
            {
                
                using HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, Url);
                httpRequest.Headers.TryAddWithoutValidation("Accept", "application/json");
                httpRequest.Headers.TryAddWithoutValidation("Accesstoken", Accesstoken);

                httpRequest.Headers.TryAddWithoutValidation("Host", "steampy.com");
                httpRequest.Headers.TryAddWithoutValidation("Referer", "https://steampy.com/cdKey/cdKey");

                using HttpResponseMessage httpResponse = await httpHelper.MyHttplClient.SendAsync(httpRequest);

                if (httpResponse == null )
                {
                    throw new Exception("请求失败");
                }
                string resStr = await httpResponse.Content.ReadAsStringAsync();
                LogHelper.LogInfo($@"
【SteamPY调用请求】
【Request】:{JsonConvert.SerializeObject(this)}
【Response】:{resStr}
");
                var res = JsonConvert.DeserializeObject<PY_BaseResponse<T>>(resStr);
                return res ?? new PY_BaseResponse<T>();
            }
            catch (Exception ex)
            {
                var error = $@"
【SteamPY调用请求失败】
【Request】:{JsonConvert.SerializeObject(this)}
【Exception】:{ex.Message}
";
                LogHelper.LogError(error);
                Console.WriteLine(error);
                throw new Exception(error);
            }
        }

    }
}
