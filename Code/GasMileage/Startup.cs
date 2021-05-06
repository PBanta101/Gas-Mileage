using GasMileage.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GasMileage
{
   public class Startup
   {
      //   F i e l d s   &   P r o p e r t i e s

      public IConfiguration Configuration { get; }


      //   C o n s t r u c t o r s

      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }


      //   M e t h o d s

      public void ConfigureServices(IServiceCollection services)
      {
         services.AddDbContext<AppDbContext>
            (options => options.UseSqlServer(Configuration.GetConnectionString("LocalDb")));

         services.AddScoped<IFillupRepository,  EfFillupRepository>();
         services.AddScoped<IUserRepository,    EfUserRepository>();
         services.AddScoped<IVehicleRepository, EfVehicleRepository>();

         services.AddControllersWithViews();

         services.AddHttpContextAccessor();

         services.AddMemoryCache();
         // services.AddDistributedMemoryCache();
         services.AddSession(
         //   options =>
         //   {
         //      options.IdleTimeout = TimeSpan.FromSeconds(10);
         //   }
         );
      }

      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
         }
         app.UseHttpsRedirection();
         app.UseStaticFiles();

         app.UseRouting();

         app.UseSession();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
         });
      }
   }
}
