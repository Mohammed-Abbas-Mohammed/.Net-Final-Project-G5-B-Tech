
using ApplicationB.Contracts_B;
using ApplicationB.Contracts_B.Category;
using ApplicationB.Contracts_B.General;
using ApplicationB.Contracts_B.Product;
using ApplicationB.Mapper_B;
using ApplicationB.Services_B;
using ApplicationB.Services_B.Category;
using ApplicationB.Services_B.General;
using ApplicationB.Services_B.Product;
using AutoMapper;
using DbContextB;
using InfrastructureB.Category;
using InfrastructureB.General;
using InfrastructureB.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using ModelsB.Authentication_and_Authorization_B;
using System.Globalization;

namespace CustomerWebsite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<BTechDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUserB, IdentityRole>().AddEntityFrameworkStores<BTechDbContext>()
                   .AddDefaultTokenProviders();



            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();


            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            var serviceProvider = builder.Services.BuildServiceProvider();
            var mapper = serviceProvider.GetService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            builder.Services.AddHttpContextAccessor();


            #region AddScoped

            builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
            builder.Services.AddScoped<ILanguageService, LanguageService>();
            builder.Services.AddScoped<IUserService, UserService>();


            //==========Product==========

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
            builder.Services.AddScoped<IProductImageService, ProductImageService>();

            builder.Services.AddScoped<IProductSpecificationRepository, ProductSpecificationRepository>();
            builder.Services.AddScoped<IProductSpecificationService, ProductSpecificationService>();
            builder.Services.AddScoped<IProductTranslationRepository, ProductTranslationRepository>();
            builder.Services.AddScoped<IProductTranslationService, ProductTranslationService>();
            builder.Services.AddScoped<IProductSpecificationTranslationRepository, ProductSpecificationTranslationRep>();
            builder.Services.AddScoped<IProductSpecificationTransService, ProductSpecificationTransService>();
            builder.Services.AddScoped<ISpecificationStoreRepository, SpecificationStoreRepository>();
            builder.Services.AddScoped<ISpecificationStoreService, SpecificationStoreService>();


            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IReviewService, ReviewService>();


            //==========Category==========

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            #endregion



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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
