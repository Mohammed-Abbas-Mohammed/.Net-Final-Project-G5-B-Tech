﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.OrderBDTOs.PaymentDTO
{
    public class AddOrUpdatePaymentBDTO
    {
        public int Id { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = "PayPal";
        [MaxLength(50)]
        public string PaymentStatus { get; set; }  // e.g., "Success", "Failed"
        [MaxLength(100)]
        public string TransactionId { get; set; }
        [MaxLength(500)]
        public string GatewayResponse { get; set; }
        public int OrderId { get; set; }
    }
}
