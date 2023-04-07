using Bloggie.Web.Data;
using Bloggie.Web.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web
{
    // Program.cs dosyasý uygulamanýn konfigürasyon ayarlarýnýn yapýldýðý, uygulamanýn baþladýðý dosyadýr.
    // .NET 5.0 ise startup
    // üstü ise program.cs
    // Builder adýnda bir middlewaremiz var bu middleware ile programda kullanacaðýmýz tüm servileri tanýmlamaktayýz. Ýleride kullanacaðýmýz tüm ek servisleri bu aanda tanýmlýyor olacaðýz. 
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // DB connection yapýldý . Bu Connection string üzerinden yaptýk
            builder.Services.AddDbContext<BloggieDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConnectionString")));

            builder.Services.AddScoped<ITagInterface, TagRepository>(); // interfacelerin yaþayacaðý zaman dilimi. yaþam döngüsü. Request atýldýðýnda çalýþýyor. request bittinde bitiyor.
            
            var app = builder.Build(); // builderlarý bunun üstüne koy

            // Configure the HTTP request pipeline.
            // app. diye kullandýðýmýz request sürecindeki tüm özellikler aþaðýda tanýmlanmýþtýr. iþimize yarayacak tüm özeliklerin tamamýný da app. diyerek tanýmlýyor olacaðýz. Örneðin aþaðýda app.UseAuthantication() özelliði request pipeline ý tanýmlanmýþtýr. Bizler ise kimlik kontrolü yapmak istediðimizde Authantication özelliðini kullanýyor olacaðýz. Bunun için ileride app.UseAuthantication() isimli özelliði dahil edeceðiz.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // statik alanlarý kullan

            app.UseRouting(); // routing i kullan

            app.UseAuthorization();

            // http://localhost:5656/ktap/Index/6
            // controller kitap
            // action Index
            // id 6
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}