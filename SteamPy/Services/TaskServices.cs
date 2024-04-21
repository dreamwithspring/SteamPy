using steamPy.Helpers.IHelpers;

namespace steamPy.Services
{
    public class TaskServices:ITaskServices
    {
        private readonly ISteamPyServices _steamPyServices;

        private readonly IMailHelper _mailHelper;
        public TaskServices(ISteamPyServices steamPyServices, IMailHelper mailHelper)
        {
            _steamPyServices = steamPyServices;
            _mailHelper = mailHelper;
        }


        public async Task TaskWithPriceInfoAndSendMailAsync()
        {
            await _steamPyServices.RefreshAndSendMail();
        }
    }
}
