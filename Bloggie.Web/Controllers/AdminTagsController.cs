using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        // Yapmış olduğumuz MVC routing şekli şunun gibi olaaktır.
        // https://localhost:1544/AdminTags/Add
        // …./controller/action
        public IActionResult Add()
        {
            return View();
        }
    }
}
