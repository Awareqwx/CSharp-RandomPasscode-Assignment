using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace RandomPasscode.Controllers
{
    public class PasswordController : Controller
    {
        private static Random r = new Random();

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            int? number = HttpContext.Session.GetInt32("number");
            if(number is null)
            {
                HttpContext.Session.SetInt32("number", 0);
                number = 1;
                Generate();
            }
            ViewBag.word = HttpContext.Session.GetString("word");
            ViewBag.number = number;
            return View("Index");
        }

        [HttpGet]
        [Route("generate")]
        public JsonResult Generate()  
        {
            int? number = HttpContext.Session.GetInt32("number");
            if(number is null)
            {
                HttpContext.Session.SetInt32("number", 0);
                number = 0;
            }
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string word = new string(Enumerable.Repeat(chars, 14).Select(s => s[r.Next(s.Length)]).ToArray());
            HttpContext.Session.SetInt32("number", ((int)number) + 1);
            HttpContext.Session.SetString("word", word);
            Console.WriteLine(r.Next(14));
            return Json(new {number = number + 1, word = word});
        }
    }
}