using HackHeroes0._1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HackHeroes0._1.Extensions
{
    public static class ControllerExtension
    {
        public static void SetNotification(this Controller controller, string type, string message)
        {
            var notification = new Notification(type, message);
            controller.TempData["Notification"] = JsonConvert.SerializeObject(notification);
        }
    }
}
