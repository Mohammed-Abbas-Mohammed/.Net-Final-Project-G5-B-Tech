using ApplicationB.Contracts_B;
using ApplicationB.Mapper_B;
using ApplicationB.Services_B.Category.NewFolder;
using DbContextB;
using InfrastructureB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ModelsB.Authentication_and_Authorization_B;
using System.Globalization;

namespace WebApplication1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext
            builder.Services.AddDbContext<BTechDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Identity

            builder.Services.AddIdentity<ApplicationUserB, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false; // Modify as needed
            })
            .AddEntityFrameworkStores<BTechDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

            //builder.Services.AddDefaultIdentity<ApplicationUserB>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddEntityFrameworkStores<BTechDbContext>();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //may be comment
            builder.Services.AddIdentity<ApplicationUserB, IdentityRole>().AddEntityFrameworkStores<BTechDbContext>().AddDefaultTokenProviders().AddDefaultUI();

            // Add services
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddAutoMapper(typeof(CategoryMappingProfile));

            // Localization
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            // Configure request localization
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-EG")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            // Add MVC
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure middleware

            // Initialize the databaseSeedUser
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUserB>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await BTechDbContextSeed.SeedAsync(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    // Log the error or handle accordingly
                    Console.WriteLine("Error In Database Seeding");
                    Console.WriteLine(ex.Message);
                }
            }
            //using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    var services = serviceScope.ServiceProvider;
            //    try
            //    {
            //        RoleInitializer.Initialize(services).Wait();
            //    }
            //    catch (Exception ex)
            //    {
            //        // Log the error or handle accordingly
            //    }
            //}

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.UseAuthentication(); // Ensure authentication is added
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Login}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }

}
