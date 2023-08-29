using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR_App.Hubs;
using SignalR_App.Models;
using System.Diagnostics;

namespace SignalR_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<ProgressHub> progressHubContext;

        /// <summary>
        /// 進度條建構子: progressHubContext
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="_hubContext"></param>
        /// <param name="quoteHubContext"></param>
        public HomeController(
            ILogger<HomeController> logger, 
            IHubContext<ProgressHub> _hubContext)
        {
            _logger = logger;
            progressHubContext = _hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region 聊天室
        /// <summary>
        /// 聊天
        /// </summary>
        /// <returns></returns>
        public IActionResult Chat()
        {
            return View();
        }
        #endregion

        #region 進度條        

        /// <summary>
        /// 進度條
        /// </summary>
        /// <returns></returns>
        public IActionResult Progress()
        {
            return View();
        }

        /// <summary>
        /// 進度條 - 模擬會執行很久的範例
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public IActionResult StartProgress(Dictionary<string, string> inModel)
        {
            Dictionary<string, string> outModel = new Dictionary<string, string>();

            int progress = 0;
            //執行 10 秒
            for (int i = 1; i <= 10; i++)
            {
                Thread.Sleep(1000);//暫停1秒
                progress = i * 10;
                progressHubContext.Clients.Client(inModel["HubConnId"]).SendAsync("UpdProgress", progress);
            }

            outModel["ResultMsg"] = "執行完成";

            return Json(outModel);
        }

        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}