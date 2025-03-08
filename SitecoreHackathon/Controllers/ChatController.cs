using System.Threading.Tasks;
using System.Web.Mvc;
using Hackathon.Repository;

namespace SitecoreHackathon.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChatRepository _chatRepository = new ChatRepository();

        [HttpPost]
        public async Task<JsonResult> GetResponse(string userMessage)
        {
            var response = await _chatRepository.GetChatResponseAsync(userMessage);
            return Json(new { BotResponse = response });
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
