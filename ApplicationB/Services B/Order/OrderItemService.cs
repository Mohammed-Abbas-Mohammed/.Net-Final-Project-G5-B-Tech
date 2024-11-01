using ApplicationB.Contracts_B;
using ApplicationB.Contracts_B.Order;
using ApplicationB.Services_B.Product;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DTOsB.OrderBDTOs.OrderItemDTO;
using DTOsB.OrderBDTOs.ShippingDTO;
using DTOsB.OrderDTO;
using DTOsB.Shared;
using ModelsB.Order_B;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.Order
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        IProductTranslationService productRepository;
        IProductService productService;


        public OrderItemService(IOrderItemRepository _orderItemRepository, IMapper _mapper, IUserService _userService,
            IProductTranslationService _productRepository, IProductService _productService
            )
        {
            orderItemRepository = _orderItemRepository;
            mapper = _mapper;
            userService = _userService;
            productRepository = _productRepository;
            productService = _productService;
        }

        //*****************************************

        public async Task<ResultView<AddOrUpdateOrderItemBDTO>> CreateOrderItemAsync(AddOrUpdateOrderItemBDTO orderItemBDTO)
        {
            var orderItem = mapper.Map<OrderItemB>(orderItemBDTO);

            orderItem.CreatedBy = userService.GetCurrentUserId();
            orderItem.Created = DateTime.Now;

            await orderItemRepository.AddAsync(orderItem);
            return ResultView<AddOrUpdateOrderItemBDTO>.Success(orderItemBDTO);
        }

        //*****************************************

        public async Task<ResultView<SelectOrderItemBDTO>> DeleteOrderItemAsync(int id)
        {
            var existingOrderItem = await orderItemRepository.GetByIdAsync(id);
            if (existingOrderItem == null)
                return ResultView<SelectOrderItemBDTO>.Failure("Order not found. Unable to delete.");

            existingOrderItem.IsDeleted = true;
            existingOrderItem.UpdatedBy = userService.GetCurrentUserId();
            existingOrderItem.Updated = DateTime.Now;

            await orderItemRepository.UpdateAsync(existingOrderItem);
            return ResultView<SelectOrderItemBDTO>.Success(null);
        }

        //*****************************************

        public async Task<IEnumerable<SelectOrderItemBDTO>> GetAllOrderItemsAsync()
        {
            var OrderItems = await orderItemRepository.GetAllAsync();

            var orders = new List<SelectOrderItemBDTO>();
            foreach (var orderItem in OrderItems)
            {
                var productName = (await productRepository.GetTranslationsByProductIdAsync(orderItem.ProductId)).Entity.FirstOrDefault().Name;
                var product = (await productService.GetProductByIdAsync(orderItem.ProductId)).Entity;
                var item = new SelectOrderItemBDTO()
                {
                    ProductName = productName,
                    Quantity = orderItem.Quantity,
                    Price = product.Price,
                    TotalPrice = product.Price * orderItem.Quantity,
                    StockQuantity = product.StockQuantity

                };
                orders.Add(item);
            }
            return OrderItems.ProjectTo<SelectOrderItemBDTO>(mapper.ConfigurationProvider);
        }

        //*****************************************

        public async Task<ResultView<SelectOrderItemBDTO>> GetOrderItemByIdAsync(int id)
        {
            var order = await orderItemRepository.GetByIdAsync(id);
            if (order == null)
                return ResultView<SelectOrderItemBDTO>.Failure("Order not found.");

            var orderDto = mapper.Map<SelectOrderItemBDTO>(order);
            return ResultView<SelectOrderItemBDTO>.Success(orderDto);
        }

        //*****************************************

        public async Task<ResultView<AddOrUpdateOrderItemBDTO>> UpdateOrderItemAsync(AddOrUpdateOrderItemBDTO orderItemBDTO)
        {
            if (orderItemBDTO == null)
                return ResultView<AddOrUpdateOrderItemBDTO>.Failure("Order item data cannot be null.");

            var existingOrderItem = await orderItemRepository.GetByIdAsync(orderItemBDTO.Id);
            if (existingOrderItem == null)
                return ResultView<AddOrUpdateOrderItemBDTO>.Failure("Order item not found. Unable to update.");

            mapper.Map(orderItemBDTO, existingOrderItem);
            existingOrderItem.UpdatedBy = userService.GetCurrentUserId();
            existingOrderItem.Updated = DateTime.Now;

            await orderItemRepository.UpdateAsync(existingOrderItem);
            return ResultView<AddOrUpdateOrderItemBDTO>.Success(orderItemBDTO);
        }

        //*****************************************

        public async Task<IEnumerable<SelectOrderItemBDTO>> GetAllItemsOfOrderAsync(int id)
        {
            var OrderItems = await orderItemRepository.ItemsOfOrder(id);
            var res = mapper.Map< IEnumerable < SelectOrderItemBDTO >>(OrderItems);
            return res;
        }
    }
}
