namespace Bloggie.Web
{
    // Program.cs dosyasý uygulamanýn konfigürasyon ayarlarýnýn yapýldýðý, uygulamanýn baþladýðý dosyadýr.
    // .NET 5.0 ise startup
    // üstü ise program.cs
    // Builder adýnda bir middlewaremiz var bu middleware ile programda kullanacaðýmýz tüm servileri tanýmlamaktayýz
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}