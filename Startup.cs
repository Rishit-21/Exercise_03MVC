using Exercise_03.Data;
using Exercise_03.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Exercise_03
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<PartyProductDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                );
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
            //    .AddViewOptions(option=> 
            //{d
            //    option.HtmlHelperOptions.ClientValidationEnabled = false;
            //});
#endif
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IPartyRepository, PartyRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPartyAssignRepository, PartyAssignRepository>();
            services.AddScoped<IProductRateRepository, ProductRateRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();

            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Party}/{action=Party}/{id?}");
            });


        }
    }
}
