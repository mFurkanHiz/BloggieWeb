using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;
        // DbContext için dependency injection işlemi gerçekleştirildi

        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

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
        //View üzerinden kullanıcının gireceği veri kadar olan kısım için bir view model oluşturduk ve orjinal domain modeldeki kullandığımız ihtiyacımız olan properyleri içerisine ekleidk. Viewda artık bu viewmodela erişmek için en üst kısma bir model tanımlaması yapıp viewmodelımızın bulunduğu namespace'i tanımladık. Sonra da inputlardaki değişiklikleri bu viewmodel'a aktarmak için her input'a birer asp-for property'si ekledik. Bunun da amacı şuydu; sayfanın en üstünde tnaımlamış olduğumuz model içindeki propertyler'den hangilerini doldurmamız gerektiğini tanımladığımız alandı. Yani kullanıcı name bölümüne bişeyler yazdığında viewmodel içinde bulunan name kısmı dolduruluyor olacaktır. Akabinde doldurulması gereken her yer doldurulduktan sonra submit işlemi gerçekleştiğinde bu verileri view model üzerinden controller'a gönderiyor olacağız. 

        [HttpPost]
        //[ActionName("Add")]
        //public IActionResult SubmitTag()
       public IActionResult Add(AddTagRequest addTagRequest)
        {
            //var name = Request.Form["name"];
            //var displayName = Request.Form["displayName"];

            //var name = addTagRequest.Name;
            //var displayName = addTagRequest.DisplayName;


            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            bloggieDbContext.Tags.Add(tag);
            bloggieDbContext.SaveChanges();

            // submit işlemi gerçekleştikten sonra bizi Add.cshtml de tutmasını istiyoruz
            // return View("Add");
            // submit butonuna bastıktan sonra List sayfasına yönlendirildik
            return RedirectToAction("List");
        }
        // List.cshtml
        // Taglari listeleme
        [HttpGet]
        public IActionResult List()
        {
            // Use dbcontext to read tags (tagleri okuyabilmek adına dbcontextimiz ile ilişki kurduk) Akabinde bu tagleri ait olan view sayfamıza yolladık (List.cshtml)
            var tags = bloggieDbContext.Tags.ToList(); // DB deki tagsları listele
            return View(tags); // tags ları ListView a yani List.cshtml e yolluyoruz
        }

        //Edit get metodu için view sayfamıza verilerin ulaştırılacağı bir view model hazırladık. Bu view model üzerinde id name ve display name olarak üç özellik tanımladık. Metodumuza parametre olarak Guid tipinde id yolladık. Ve cshtml dosyasında model olarak bu view modeli kullandık. Daha öncesinde list sayfasındaki edit butonunda asp-route-id olarak id tanımlaması yaptığımız için doğrudan bu idye ait olan veriler ekrana controllerın aşağıdaki hali sayesinde geliyor oldu. aşağıda parametre olarak gönderilen id ile veritabanı sorgusu yaparak bu idye denk gelen objeyi çağırmış olduk ve viewmodeldaki propertyler bu objeyle eşitlendi. View sayfası da model olarak bu viewmodeli kullandığı için verilerimiz get metoduyla otomatik olarak ilgili inputlara yüklenmiş oldu. 


        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            //// 1st method
            //var tag = bloggieDbContext.Tags.Find(id);

            // 2nd method
            var tag = bloggieDbContext.Tags.FirstOrDefault(x => x.Id == id);

            if(tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };
                return View(editTagRequest);
            }
            return View(null);
        }

        //Post işlemi için ilgili edit sayfamızda model olarak kullandığımız edittagrequest isimli viewmodelımızı yine kullanıyor olacağız. Çünkü formumuz post işlemi submit etmektedir. Bu yüzden aynı isimdeki IActionResult üzerindeki HttpPost olan kısım çalışıyor olacaktır. Öncelikle Veritabanı modelimiz olan Tag model üzerinden yeni bir nesne üretim propertylerini viewmodella eşitliyoruz. Sonrasında bu idye id olan veritabanı nesnemizi bulmak için find metodu kullanıyoruz. Bu bu id ait olan nesnenin veritabanındaki prpertylerini yeni oluşturmuş olduğumuz tag nesnesinin propertyleriyle eşitleyerek yeni atamayı yapmış oluyoruz. Akabinde savechanges diyerek veritabanını güncellemiş oluyoruz.


        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest) 
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,

            };

            var existingTag = bloggieDbContext.Tags.Find(tag.Id);

            if(existingTag != null)
            {
                existingTag.Name = tag.Name; 
                existingTag.DisplayName = tag.DisplayName;

                bloggieDbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return View();
        }
    }
}
