using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Dados.DAO;
using Alura.LeilaoOnline.WebApp.Dados.DAO.EfCore;
using Alura.LeilaoOnline.WebApp.Seeding;
using Alura.LeilaoOnline.WebApp.Services;
using Alura.LeilaoOnline.WebApp.Services.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alura.LeilaoOnline.WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllersWithViews()
                .AddNewtonsoftJson(options => 
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ILeilaoDao, LeiloesDao>();
            services.AddScoped<ICategoriasDao, CategoriasDao>();

            services.AddScoped<IAdminService, DefaultAdminService>();
            services.AddScoped<IProdutoService, DefaultProdutoService>();

            var sp = services.BuildServiceProvider();
            var ctx = sp.GetService<AppDbContext>();
            DatabaseGenerator.Seed(ctx);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePagesWithRedirects("/Home/StatusCode/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
