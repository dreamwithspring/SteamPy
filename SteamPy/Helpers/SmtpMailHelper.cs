using steamPy.BaseModels;
using steamPy.Helpers.IHelpers;
using System.Net.Mail;

namespace steamPy.Helpers
{
    public class SmtpMailHelper : IMailHelper
    {
        private readonly ILogger<SmtpMailHelper> _logger;
        private readonly IConfiguration _configuration;

        public SmtpMailHelper(ILogger<SmtpMailHelper> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        /// <summary>
        ///  邮箱用户名
        /// </summary>
        private string mailUserName => _configuration["Mail:MailUserName"];
        /// <summary>
        ///  邮箱密码
        /// </summary>
        private string mailUserPassword => _configuration["Mail:MailUserPassword"];
        /// <summary>
        /// 邮件服务器
        /// </summary>
        private string emailServer => _configuration["Mail:EmailServer"];
        /// <summary>
        /// 设置为true允许安全连接本地客户端发送邮件 ,  设置为false不允许允许安全连接本地客户端发送邮件 
        /// </summary>
        private bool enableSsl => true;

        /// <summary>
        /// 发送EMAIL
        /// </summary>
        /// <param name="sRecipientEmail">收件人地址</param>
        /// <param name="sSubject">主题</param>
        /// <param name="sMessage">内容</param>
        /// <param name="sSendName">发件人名称</param>
        /// <returns>发送是否成功</returns>
        public bool SendMail(string sRecipientEmail, string sSubject, string sMessage, List<string>? files = null)
        {
            try
            {
                //邮件对象
                ;

                //smtp客户端对象 //邮件发送客户端
                using SmtpClient client = new SmtpClient();

                string sSenderEmail = mailUserName;
                using MailMessage emailMessage = new MailMessage(sSenderEmail, sRecipientEmail, sSubject, sMessage);
                emailMessage.IsBodyHtml = true;
                emailMessage.SubjectEncoding = System.Text.Encoding.Default;
                emailMessage.BodyEncoding = System.Text.Encoding.Default;

                if (files != null)
                {
                    foreach (var item in files)
                    {
                        if (File.Exists(item))
                        {
                            emailMessage.Attachments.Add(new Attachment(item));
                        }
                    }
                }

                //邮件服务器及帐户信息
                client.Host = emailServer;
                System.Net.NetworkCredential Credential = new System.Net.NetworkCredential();

                //读取邮件服务器用户名和密码信息
                Credential.UserName = mailUserName;
                Credential.Password = mailUserPassword;
                client.Credentials = Credential;
                client.EnableSsl = enableSsl;
                //发送邮件
                client.Send(emailMessage);
                emailMessage.Attachments.Dispose();
            }
            catch (Exception ex)
            {
                //错误处理待定
                _logger.LogError(ex.Message);
                return false;
            }
            return true;
        }

        public bool SendMail(MailInfo info)
        {
            return SendMail(info.EmailAddress, info.Title, info.Message, info.Files);
        }
    }
}
