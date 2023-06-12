using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Lab3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SongForm() => View();

        [HttpPost]
        public IActionResult Sing()
        {  
                ViewBag.count = int.Parse(Request.Form["num"]);
                return View();
        
        }

        public IActionResult CreateStudent() => View();

        [HttpPost]
        public IActionResult DisplayStudent(Student student)
        {
            var properties = student.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(student);
                if (value == null)
                {
                    return View("Error");
                }
            }
            return View(student);
        }
        public IActionResult Error()
        {
            return View();
        }

    }
}
