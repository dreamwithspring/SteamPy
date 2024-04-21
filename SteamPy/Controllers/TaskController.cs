using Hangfire;
using Microsoft.AspNetCore.Mvc;
using steamPy.BaseModels;
using steamPy.Helpers.IHelpers;
using steamPy.Services;
using System.Diagnostics.Metrics;

namespace steamPy.Controllers
{
    [Route("Task/[action]")]
    public class TaskController : Controller
    {
        private readonly ITaskServices _taskServices;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IMailHelper _mailHelper;
        private readonly IConfiguration _configuration;

        public TaskController(ITaskServices taskServices, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager, IMailHelper mailHelper, IConfiguration configuration)
        {
            _taskServices = taskServices;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
            _mailHelper = mailHelper;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task StartSteamPyTask([FromForm] string cron = "0 0/5 * * * ? ")
        {

            _recurringJobManager.AddOrUpdate<ITaskServices>($"AutoSteamPy", x => x.TaskWithPriceInfoAndSendMailAsync(), cron);
        }

        [HttpGet]
        public async Task StartSteamPyTaskTest()
        {
            await _taskServices.TaskWithPriceInfoAndSendMailAsync();
        }


        [HttpGet]
        public async Task TestSendMail()
        {
            _mailHelper.SendMail(new MailInfo
            {
                EmailAddress = _configuration["Mail:TestAddress"],
                Message = "这是一封测试邮件",
                Title = "Test",
            });
        }
    }
}
