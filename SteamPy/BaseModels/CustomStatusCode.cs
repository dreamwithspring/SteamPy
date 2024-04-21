using System.ComponentModel;

namespace steamPy.BaseModels
{
    public enum CustomStatusCode
    {
        #region 2xx

        /// <summary>
        /// 正常返回
        /// </summary>
        [Description("OK！")]
        Ok200 = 200,

        /// <summary>
        /// 请求已经被实现
        /// </summary>
        [Description("请求已经被实现！")]
        Created201 = 201,

        /// <summary>
        /// 服务器已接受请求，但尚未处理，异步
        /// </summary>
        [Description("异步处理！")]
        Accepted202 = 202,




        /// <summary>
        /// 服务器成功处理了请求，但不需要返回任何实体内容
        /// </summary>
        [Description("成功处理了请求，不需要返回任何实体内容！")]
        No_Content204 = 204,

        #endregion

        #region 3xx




        #endregion

        #region 4xx

        /// <summary>
        /// 语义有误，当前请求无法被服务器理解
        /// </summary>
        [Description("请求错误！")]
        Bad_Request400 = 400,

        /// <summary>
        /// 未登录
        /// </summary>
        [Description("未登录！")]
        Unauthorized401 = 401,

        /// <summary>
        /// 拒绝访问
        /// </summary>
        [Description("拒绝访问！")]
        Forbidden403 = 403,

        /// <summary>
        /// 资源未找到
        /// </summary>
        [Description("资源未找到！")]
        NotFound404 = 404,

        /// <summary>
        /// 请求方法不能被用于请求相应的资源
        /// </summary>
        [Description("请求方法不能被用于请求相应的资源！")]
        Method_Not_Allowed405 = 405,

        /// <summary>
        /// 请求的资源的内容特性无法满足请求头中的条件
        /// </summary>
        [Description("请求的资源的内容特性无法满足请求头中的条件！")]
        Not_Acceptable406 = 406,


        /// <summary>
        /// 请求超时
        /// </summary>
        [Description("请求超时！")]
        Request_Timeout408 = 408,

        /// <summary>
        /// 请求中提交的实体并不是服务器中所支持的格式
        /// </summary>
        [Description("请求中提交的实体并不是服务器中所支持的格式！")]
        Unsupported_Media_Type415 = 415,

        /// <summary>
        /// 当前资源被锁定
        /// </summary>
        [Description("当前资源被锁定！")]
        Locked423 = 423,

        #endregion

        #region 5xx

        /// <summary>
        /// 服务器错误
        /// </summary>
        [Description("服务器错误！")]
        Error500 = 500,

        #endregion

        #region 9xx 用户自定义


        /// <summary>
        /// 其它错误
        /// </summary>
        [Description("其它错误！")]
        OtherError900 = 900,


        #region 901 程序主动返回错误

        /// <summary>
        /// 系统主动返回错误
        /// </summary>
        [Description("系统主动返回错误！")]
        MessageError901 = 901,

        #endregion



        #region 902 验证错误

        /// <summary>
        /// 验证错误
        /// </summary>
        [Description("模型验证错误！")]
        ValidationError902 = 902,


        /// <summary>
        /// 模型验证错误
        /// </summary>
        [Description("模型验证错误！")]
        ValidationModelError9020 = 9020,


        /// <summary>
        /// token校验错误
        /// </summary>
        [Description("token校验错误！")]
        ValidationTokenError9021 = 9021,

        #endregion

        #region 903 程序执行错误

        /// <summary>
        /// 程序执行错误
        /// </summary>
        [Description("程序执行错误！")]
        ExecuteError903 = 903,



        #endregion

        #region 904 插件或者组件执行错误

        /// <summary>
        /// 插件或者组件执行错误
        /// </summary>
        [Description("插件或者组件执行错误！")]
        PluginError904 = 904,


        /// <summary>
        /// Quartz执行错误
        /// </summary>
        [Description("Quartz执行错误！")]
        QuartzPluginError9041 = 9041,


        #endregion

        #endregion


    }
}
