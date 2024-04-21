using Newtonsoft.Json;
using steamPy.Helpers.Extensions;

namespace steamPy.BaseModels
{
    /// <summary>
    /// 自定义的返回
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class CustomResult<TData>
    {
        public CustomResult()
        {
            StatusCode = CustomStatusCode.Ok200;
        }
        /// <summary>
        /// 自定义状态码
        /// </summary>
        public CustomStatusCode StatusCode { get; set; }


        public string StatusMessage
        {
            get => StatusCode.GetEnumDescription();
        }


        public string Message { get; set; }

        public TData Data { get; set; }

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public bool Succeeded
        {
            get
            {
                return StatusCode == CustomStatusCode.Ok200;
            }
        }
    }

    public class CustomResult : CustomResult<object>
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        /// <typeparam name="TData">数据类型</typeparam>
        /// <param name="code">返回的状态码</param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static CustomResult<TData> ReturnResult<TData>(CustomStatusCode code, TData data, string message = "")
        {
            return new CustomResult<TData>()
            {
                Message = message,
                Data = data,
                StatusCode = code,
            };
        }

        /// <summary>
        /// 返回数据
        /// </summary>
        /// <typeparam name="TData">数据类型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static CustomResult<TData> ReturnResult<TData>(TData data, string message = "")
        {
            return new CustomResult<TData>()
            {
                Message = message,
                Data = data,
                StatusCode = CustomStatusCode.Ok200,
            };
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static CustomResult<string> ReturnError(string message, CustomStatusCode code = CustomStatusCode.OtherError900)
        {
            return new CustomResult<string>()
            {
                Message = message,
                StatusCode = code,
            };
        }


        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static CustomResult<string> ReturnError(CustomStatusCode code = CustomStatusCode.OtherError900)
        {
            return new CustomResult<string>()
            {
                StatusCode = code,
            };
        }

        /// <summary>
        /// 返回OK
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static CustomResult<object?> ResultOk(object? data, string message = "",
            CustomStatusCode code = CustomStatusCode.Ok200)
        {

            return new CustomResult<object?>()
            {
                Data = data,
                Message = message,
                StatusCode = code,
            };
        }

        /// <summary>
        /// 返回OK
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static CustomResult<string> ResultOk(string message = "",
            CustomStatusCode code = CustomStatusCode.Ok200)
        {

            return new CustomResult<string>()
            {
                Message = message,
                StatusCode = code,
            };
        }
    }
}
