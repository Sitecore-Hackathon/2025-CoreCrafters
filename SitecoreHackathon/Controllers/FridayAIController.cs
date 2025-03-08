using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc; // Add this reference

namespace SitecoreHackathon.Controllers
{
    public class FridayAIController : Controller // Must inherit from Controller
    {
        [HttpPost]
        public ActionResult SendMessage(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Index"); // Ignore empty messages
            }
            // Retrieve previous messages from session
            var messages = (List<string>)Session["ChatMessages"] ?? new List<string>();
            // Simulated AI Response (Replace with actual Gemini API call)
            string response = $"You said: {query}.!";
            // Add user and AI messages to the chat history
            messages.Add($"<b>You:</b> {query}");
            messages.Add($"<b>Gemini:</b> {response}");
            // Save back to session
            Session["ChatMessages"] = messages;
            return RedirectToAction("Index"); // Reload the chat
        }

        [HttpGet] // Add this attribute to clarify it's a GET action
        public ActionResult Index()
        {
            // Pass messages to view if needed
            ViewBag.Messages = Session["ChatMessages"] as List<string> ?? new List<string>();
            return View();
        }

        [HttpGet]
        public ActionResult ChatHistory()
        {
            ViewBag.Messages = Session["ChatMessages"] as List<string> ?? new List<string>();
            return PartialView("_ChatMessage");
        }
    }
}