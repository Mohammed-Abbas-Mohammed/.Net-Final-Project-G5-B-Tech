using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsB.Product_B
{
    public class SellerB
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [MaxLength(200)]
        public string? WebsiteUrl { get; set; }
        [MaxLength(75)]
        public string CommercialRegister { get; set; }
        [MaxLength(75)]
        public string TaxCard { get; set; }

        [MaxLength(75)]
        public string VATRegistrationCertificate { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; } // Brief info about the seller
        [MaxLength(75)]
        public string? StoreName { get; set; }
        [MaxLength(250)]
        public string? ProfileImageUrl { get; set; }
        public DateTime RegisteredDate { get; set; }
        public List<ProductB> Products { get; set; }
    }
}
