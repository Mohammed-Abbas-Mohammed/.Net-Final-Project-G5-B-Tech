﻿using DTOsB.OrderBDTOs.OrderItemDTO;
using ModelsB.Order_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Contracts_B.Order
{
    public interface IOrderItemRepository : IGenericRepositoryB<OrderItemB>
    {
        public Task<IQueryable<OrderItemB>> ItemsOfOrder(int id);
    }
}
