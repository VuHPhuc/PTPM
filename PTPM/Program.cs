using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Security.Policy;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using PTPM.Models;

namespace PTPM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            //Ket noi sql
            {
                var connectString = builder.Configuration.GetConnectionString("PTPM");
                builder.Services.AddDbContext<PtpmContext>(options => options.UseSqlServer(connectString));

                builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));

                builder.Services.AddControllersWithViews();
                builder.Services.AddSession();
                //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                //      .AddCookie(p =>
                //      {

                //          p.LoginPath = "/dang-nhap.html";
                //          p.LogoutPath = "/dang-xuat/html";
                //          p.AccessDeniedPath = "/not-found.html";
                //      });
                //builder.Services.AddMvc(o =>
                //{
                //    o.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
                //});
                builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });
            }


            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddHttpClient<ServiceCollection>(); //Thêm
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.MapRazorPages();

            app.Run();
        }
    }
}