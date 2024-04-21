using steamPy.BaseModels;
using System.Dynamic;

namespace steamPy.Helpers.IHelpers
{
    public interface IMailHelper
    {
        /// <summary>
        /// 发送EMAIL
        /// </summary>
        /// <param name="sRecipientEmail">收件人地址</param>
        /// <param name="sSubject">主题</param>
        /// <param name="sMessage">内容</param>
        /// <returns>发送是否成功</returns>
        bool SendMail(string sRecipientEmail, string sSubject, string sMessage, List<string>? files = null);
        
        bool SendMail(MailInfo info);
    }
}
