namespace steamPy.BaseModels
{
    /// <summary>
    /// 邮件信息
    /// </summary>
    public class MailInfo
    {
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 文件地址
        /// </summary>
        public List<string> Files { get; set; }
    }
}
