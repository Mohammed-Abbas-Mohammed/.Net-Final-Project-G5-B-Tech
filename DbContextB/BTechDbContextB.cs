using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ModelsB;
using ModelsB.Authentication_and_Authorization;
using ModelsB.Category;
using ModelsB.Localization;
using ModelsB.Product;
using ModelsB.Shared;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace DbContextB
{
    public class BTechDbContextB : IdentityDbContext<ApplicationUserB>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BTechDbContextB(DbContextOptions<BTechDbContextB> options , IHttpContextAccessor httpContextAccessor)
        : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserB>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");

            builder.Entity<ProductB>().HasIndex(p => p.CategoryId);
            builder.Entity<ProductTranslationB>().HasIndex(pt => new { pt.ProductId, pt.LanguageId }).IsUnique();

            builder.Entity<ProductTranslationB>().HasIndex(pt => new { pt.ProductId, pt.LanguageId }).IsUnique();  

            
            builder.Entity<ProductSpecificationTranslationB>().HasIndex(pst => new { pst.SpecificationId, pst.LanguageId })
                .IsUnique();
        }

        /// <summary>
        /// Product DbSets
        /// </summary>
        public virtual DbSet<ProductB> Products { get; set; }
        public virtual DbSet<ProductImageB> ProductImages { get; set; }
        public virtual DbSet<ProductTranslationB> ProductTranslations { get; set; }
        public virtual DbSet<ProductSpecificationsB> ProductSpecifications { get; set; }
        public virtual DbSet<ProductSpecificationTranslationB> ProductSpecificationTranslations { get; set; }



        public virtual DbSet<CategoryB> Categories { get; set; }

        public virtual DbSet<OrderB> Orders { get; set; }


        public virtual DbSet<CartItemB> CartItems { get; set; }

        public virtual DbSet<CouponB> Coupons { get; set; }

        public virtual DbSet<DiscountB> Discounts { get; set; }

        public virtual DbSet<PaymentB> Payments { get; set; }

        public virtual DbSet<ProductOrderB> ProductOrders { get; set; }

        public virtual DbSet<PromotionB> Promotions { get; set; }

        public virtual DbSet<ReviewB> Reviews { get; set; }

        public virtual DbSet<SellerB> Sellers { get; set; }

        public virtual DbSet<ShippingB> Shippings { get; set; }

        public virtual DbSet<ShoppingCartB> ShoppingCarts { get; set; }

        public virtual DbSet<TokenModelB> TokenModels { get; set; }

        public virtual DbSet<LocalizationResourceB> LocalizationResources { get; set; }
        public virtual DbSet<EntityLogB> EntityLogs { get; set; }

        public virtual DbSet<LanguageB> Languages { get; set; }




        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    var userId = GetCurrentUserId(); // Method to get the current user's ID

        //    foreach (var entry in ChangeTracker.Entries<BaseEntityB>())
        //    {
        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Entity.CreatedBy = userId;
        //            entry.Entity.Created = DateTime.Now;
        //            entry.Entity.IsDeleted = false; 
        //        }

        //        if (entry.State == EntityState.Modified)
        //        {
        //            entry.Entity.UpdatedBy = userId;
        //            entry.Entity.Updated = DateTime.Now;
        //        }
        //    }
        //    return await base.SaveChangesAsync(cancellationToken);
        //}

        //private int GetCurrentUserId()
        //{
        //    var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    return userId != null ? int.Parse(userId) : 0;
        //}
    }
}
