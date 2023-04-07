using Bloggie.Web.Data;
using Bloggie.Web.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web
{
    // Program.cs dosyas� uygulaman�n konfig�rasyon ayarlar�n�n yap�ld���, uygulaman�n ba�lad��� dosyad�r.
    // .NET 5.0 ise startup
    // �st� ise program.cs
    // Builder ad�nda bir middlewaremiz var bu middleware ile programda kullanaca��m�z t�m servileri tan�mlamaktay�z. �leride kullanaca��m�z t�m ek servisleri bu aanda tan�ml�yor olaca��z. 
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // DB connection yap�ld� . Bu Connection string �zerinden yapt�k
            builder.Services.AddDbContext<BloggieDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConnectionString")));

            builder.Services.AddScoped<ITagInterface, TagRepository>(); // interfacelerin ya�ayaca�� zaman dilimi. ya�am d�ng�s�. Request at�ld���nda �al���yor. request bittinde bitiyor.
            
            var app = builder.Build(); // builderlar� bunun �st�ne koy

            // Configure the HTTP request pipeline.
            // app. diye kulland���m�z request s�recindeki t�m �zellikler a�a��da tan�mlanm��t�r. i�imize yarayacak t�m �zeliklerin tamam�n� da app. diyerek tan�ml�yor olaca��z. �rne�in a�a��da app.UseAuthantication() �zelli�i request pipeline � tan�mlanm��t�r. Bizler ise kimlik kontrol� yapmak istedi�imizde Authantication �zelli�ini kullan�yor olaca��z. Bunun i�in ileride app.UseAuthantication() isimli �zelli�i dahil edece�iz.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // statik alanlar� kullan

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