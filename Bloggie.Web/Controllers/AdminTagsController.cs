using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        // Yapmış olduğumuz MVC routing şekli şunun gibi olaaktır.
        // https://localhost:1544/AdminTags/Add
        // …./controller/action
        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }
        // Http Post metodu çalıştırıyoruz. Burada amacımız Add View'ında oluşturmuş olduğumuz form içerisinde gerçekleşen POST metodu butona tıklandığında submit olsun ve bu submit sonrasında aşağıdaki actionumuz devreye girsin.
        // peki kullanıcının bu bahsi geçen view üzerinde inputlara girmiş olduğu verileri sayfamıza nasıl çağırırız?
        // 1- Bunun için önce view a gideceğiz ve inputlar abirer name vereceğiz. Sonra da controllerların içerisinde birer değişkende formdan gelen inputları aşağıdaki gibi tutacağız. Fakat get metodu kullandığımız add actionu ile post metodunda add actionunun adları aynı olamamaktadır ünkü bir metot aynı sayıda parametreyi alarak yeniden aynı isimde kullanılamamaktadır. dolayısıyla biz de metodumuzun adının farklı olmasını fakat davranış biçiinin aynı olmasını sağlamak adına action aname adında bir özellik kullanarak bu metodun da add actionu içinde davranış sergilemesini sağladık.
        [HttpPost]
        [ActionName("Add")]
        public IActionResult SubmitTag()
        {
            var name = Request.Form["name"];
            var displayName = Request.Form["displayName"];
            // submit işlemi gerçekleştikten sonra bizi Add.cshtml de tutmasını istiyoruz
            return View("Add");
        }
    }
}
