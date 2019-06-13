using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RPPP12
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json",
            optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
            optional: true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var appSection = Configuration.GetSection("AppSettings");
            string connectionString = appSection["ConnectionString"];
            services.AddDbContext<Models.RPPP12Context>(
            options => options.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}"
                );
                routes.MapRoute(
                    name: "dionica",
                    template: "{controller=Dionica}/{action=Index}"
                );
                routes.MapRoute(
                    name: "naplatnapostaja",
                    template: "{controller=NaplatnaPostaja}/{action=Index}"
                );
                routes.MapRoute(
                    name: "naplatnakucica",
                    template: "{controller=NaplatnaKucica}/{action=Index}"
                );
                routes.MapRoute(
                    name: "uredaj",
                    template: "{controller=Uredaj}/{action=Index}"
                );
                routes.MapRoute(
                    name: "dogadaj",
                    template: "{controller=Dogadaj}/{action=Index}"
                );
                routes.MapRoute(
                    name: "upravitelj",
                    template: "{controller=Upravitelj}/{action=Index}"
                );
                routes.MapRoute(
                    name: "zaposlenik",
                    template: "{controller=Zaposlenik}/{action=Index}"
                );
                routes.MapRoute(
                    name: "alarm",
                    template: "{controller=Alarm}/{action=Index}"
                );
                routes.MapRoute(
                    name: "autocesta",
                    template: "{controller=Autocesta}/{action=Index}"
                );
                routes.MapRoute(
                    name: "cjenik",
                    template: "{controller=Cjenik}/{action=Index}"
                );
                routes.MapRoute(
                    name: "kategorijaScenarija",
                    template: "{controller=KategorijaScenarija}/{action=Index}"
                );
                routes.MapRoute(
                    name: "kategorijaVozila",
                    template: "{controller=KategorijaVozila}/{action=Index}"
                );
                routes.MapRoute(
                    name: "lokacijaAutoceste",
                    template: "{controller=LokacijaAutoceste}/{action=Index}"
                );
                routes.MapRoute(
                    name: "lokacijaPostaje",
                    template: "{controller=LokacijaPostaje}/{action=Index}"
                );
                routes.MapRoute(
                    name: "nacinPlacanja",
                    template: "{controller=NacinPlacanja}/{action=Index}"
                );
                routes.MapRoute(
                    name: "objekt",
                    template: "{controller=Objekt}/{action=Index}"
                );
                routes.MapRoute(
                    name: "racun",
                    template: "{controller=Racun}/{action=Index}"
                );
                routes.MapRoute(
                    name: "razinaOpasnosti",
                    template: "{controller=RazinaOpasnosti}/{action=Index}"
                );
                routes.MapRoute(
                    name: "scenarij",
                    template: "{controller=Scenarij}/{action=Index}"
                );
                routes.MapRoute(
                    name: "sjediste",
                    template: "{controller=Sjediste}/{action=Index}"
                );
                routes.MapRoute(
                    name: "stanje",
                    template: "{controller=Stanje}/{action=Index}"
                );
                routes.MapRoute(
                    name: "sustavNaplate",
                    template: "{controller=SustavNaplate}/{action=Index}"
                );
                routes.MapRoute(
                    name: "vrstaNaplatneKucice",
                    template: "{controller=VrstaNaplatneKucice}/{action=Index}"
                );
                routes.MapRoute(
                    name: "vrstaObjekta",
                    template: "{controller=VrstaObjekta}/{action=Index}"
                );
                routes.MapRoute(
                    name: "vrstaUredaja",
                    template: "{controller=VrstaUredaja}/{action=Index}"
                );
                routes.MapRoute(
                    name: "vrstaZaposlenika",
                    template: "{controller=VrstaZaposlenika}/{action=Index}"
                );
                routes.MapRoute(
                    name: "zabrana",
                    template: "{controller=Zabrana}/{action=Index}"
                );
            
            });  
        }
    }
}
