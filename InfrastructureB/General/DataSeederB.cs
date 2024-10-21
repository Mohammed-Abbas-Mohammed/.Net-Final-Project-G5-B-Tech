using ModelsB.Category_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbContextB;
using Microsoft.Extensions.DependencyInjection;
using Bogus;
using ModelsB.Product_B;
using ModelsB.Authentication_and_Authorization_B;

namespace InfrastructureB.General
{
    public class DataSeederB
    {
        private readonly BTechDbContext _context;

        public DataSeederB(BTechDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {

            _context.Database.EnsureCreated();

            //var categoryFaker = new Faker<ApplicationUserB>()
            //    .RuleFor(c => c.Email, f => f.);

            //var categoryFaker = new Faker<CategoryB>()
            //    .RuleFor(c => c.ImageUrl, f => f.Commerce.Categories(1)[0]);


            //var categories = categoryFaker.Generate(10);  // Generate 10 fake categories

            //// Add categories to the context
            //_context.Categories.AddRange(categories);
            //_context.SaveChanges();

            //// Similarly, you can generate fake data for other entities like ProductB, OrderB, etc.
            //var productFaker = new Faker<ProductB>()
            //.RuleFor(p => p., f => f.Commerce.ProductName())
            //.RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()))
            //.RuleFor(p => p.Description, f => f.Commerce.ProductDescription());

            //var products = productFaker.Generate(20);  // Generate 20 fake products
            //_context.Products.AddRange(products);
            _context.SaveChanges();
        }
    }
}
