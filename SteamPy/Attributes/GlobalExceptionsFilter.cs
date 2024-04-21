using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using steamPy.BaseModels;

namespace steamPy.Attributes
{
    /// <summary>
    /// 全局异常过滤器
    /// </summary>
    public class GlobalExceptionsFilter : ExceptionFilterAttribute
    {
        private readonly IHostEnvironment env;
        private readonly ILogger<GlobalExceptionsFilter> _logHelper;

        public GlobalExceptionsFilter(IHostEnvironment _env, ILogger<GlobalExceptionsFilter> logHelper)
        {
            env = _env;
            _logHelper = logHelper;
        }

        public override void OnException(ExceptionContext context)
        {
            CustomResult<object> oper = new CustomResult<object>();

            oper.StatusCode = CustomStatusCode.OtherError900;


            oper.Message = context.Exception.Message;/*错误信息*/
            if (env.IsDevelopment())
            {
                oper.Message += context.Exception.StackTrace;/*堆栈信息*/
            }

            var res = new ContentResult
            {
                Content = JsonConvert.SerializeObject(oper)
            };
            context.Result = res;

            /* 日志记录 */
            _logHelper.LogError("全局捕获异常", context.Exception);
        }
    }
}
