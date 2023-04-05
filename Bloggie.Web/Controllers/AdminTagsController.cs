using Bloggie.Web.Models.ViewModels;
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
        // 2- bir viewModel tanımlanır. ve view'da kullanıcının girdiği veriler bu viewModela aktarılır. Amaç domain modelin bir kopyasını oluşturmak ve viewdan gelen verileri buraya aktarmaktır. Daha sonra veritabanına bu verğleri aktarırken viewmodelden çekip aktarıyor olacağız.
        // View üzerinden kullanıcının gireceği veri kadar olan kısım için bir view model oluşturduk ve orijinal v.. propertyleri içerisine ekledik. viewda artık bu vewmodal a erişmek için en üt kısma ......

        [HttpPost]
        //[ActionName("Add")]
        //public IActionResult SubmitTag()
       public IActionResult Add(AddTagRequest addTagRequest)
        {
            //var name = Request.Form["name"];
            //var displayName = Request.Form["displayName"];

            var name = addTagRequest.Name;
            var displayName = addTagRequest.DisplayName;

            // submit işlemi gerçekleştikten sonra bizi Add.cshtml de tutmasını istiyoruz
            return View("Add");
        }
    }
}
