using ModelsB.Order_B;
using ModelsB.Product_B;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.OrderBDTOs.OrderItemDTO
{
    public class AddOrUpdateOrderItemBDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        [MaxLength(100)]
        public string UpdatedBy { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
        public bool? IsDeleted { get; set; } = false;
    }
}
