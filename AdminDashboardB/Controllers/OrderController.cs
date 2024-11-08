using ApplicationB.Services_B.Order;
using AutoMapper;
using DTOsB.Shared;
using DTOsB.Order.OrderDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DTOsB.Controllers
{
    [Authorize(Roles = "Admin")]

    public class OrderController : Controller
    {
        private readonly IOrderService orderService; // Assuming you're using a service for business logic

        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService;
        }
        //==============All=====================
        public async Task<IActionResult> Index()
        {
            var orders = await orderService.GetAllOrdersAsync();
            return View(orders);
        }

        //==============Add=====================

        public async Task<IActionResult> Add()
        {
            AddOrUpdateOrderBDTO order = new AddOrUpdateOrderBDTO();
            // order.Users = 

            return View("Add", order);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOrUpdateOrderBDTO orderBDTO)
        {
            /* (ModelState.IsValid)
            {*/
            orderBDTO.OrderDate = DateTime.Now;
            orderBDTO.TotalPrice = 5000;
            orderBDTO.ApplicationUserId = "db0a8336-7f0f-416c-90c8-a8dfd01d97f7";


            await orderService.CreateOrderAsync(orderBDTO);
            return RedirectToAction("Index");
            /*}

            return View(orderBDTO);*/
        }

        //==============Edit=====================

        public async Task<IActionResult> Edit(int id)
        {
            SelectOrderBDTO order = await orderService.GetOrderByIdAsync(id);

            if (order == null || order.CurrentStatus == ModelsB.Order_B.Status.Shipped || order.CurrentStatus == ModelsB.Order_B.Status.Delivered)
            {
                TempData["ErrorMessage"] = "You can't edit this order anymore.";
                return RedirectToAction("Index");
            }
            var updateOrder = new AddOrUpdateOrderBDTO()
            {
                Id = id,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                CurrentStatus = order.CurrentStatus,
                ApplicationUserId = "db0a8336-7f0f-416c-90c8-a8dfd01d97f7"
            };

            return View("Edit", updateOrder);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddOrUpdateOrderBDTO orderBDTO)
        {
            if (ModelState.IsValid)
            {
                await orderService.UpdateOrderAsync(orderBDTO);
                return RedirectToAction("Index");
            }

            return View(orderBDTO);
        }

        //==============Details=====================

        public async Task<IActionResult> DetailsOfOrder(int id)
        {
            var order = await orderService.GetOrderByIdAsync(id);
            foreach (var item in order.OrderItems)
            {
                item.TotalPrice = (int)(item.Quantity * item.Price);
            }
            return View("Details", order);
        }

        //==============Cancel=====================

        public async Task<IActionResult> CancelOrder(int id)
        {
            var order = await orderService.GetOrderByIdAsync(id);
            if (order == null || order.CurrentStatus == ModelsB.Order_B.Status.Shipped || order.CurrentStatus == ModelsB.Order_B.Status.Delivered)
            {
                TempData["ErrorMessage"] = "You can't cancel this order anymore.";
                return RedirectToAction("Index");
            }

            await orderService.DeleteOrderAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
