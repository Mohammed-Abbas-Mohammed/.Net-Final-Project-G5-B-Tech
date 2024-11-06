using ApplicationB.Services_B.Order;
using AutoMapper;
using DTOsB.Order.OrderDTO;
using DTOsB.Order.PaymentDTO;
using DTOsB.Order.ShippingDTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/paypal")]
[ApiController]
public class PayPalController : ControllerBase
{
    private readonly IShippingService shippingService;
    private readonly IPaymentService paymentService;
    private readonly PayPalService _payPalService;
    private readonly IOrderService orderService;
    private IMapper mapper;

    public PayPalController(PayPalService payPalService, IOrderService _orderService, IMapper _mapper,
                            IShippingService _shippingService, IPaymentService _paymentService)
    {
        _payPalService = payPalService;
        orderService = _orderService; 
        mapper = _mapper;
        shippingService = _shippingService;
        paymentService = _paymentService;
    }

    [HttpPost("complete-payment")]
    public async Task<IActionResult> CompletePayment([FromBody] PayPalOrderRequest request, int orderId, decimal shipCost, string shipAddress)
    {
        var result = await _payPalService.CaptureOrderAsync(request.OrderID);

        if (result != null && result.CurrentStatus == ModelsB.Order_B.Status.Delivered)
        {
            //===========(1) change the status of order to Pending ===========
            var order = await orderService.GetOrderByIdAsync(orderId);

            if (order == null || order.IsDeleted == true)
            {
                return NotFound("Order not found.");
            }
            order.CurrentStatus = ModelsB.Order_B.Status.Pending;
            var updateOrder = mapper.Map<AddOrUpdateOrderBDTO>(order);
            await orderService.UpdateOrderAsync(updateOrder);

            //===========(2) add to payment table ===========
            var payment = new AddOrUpdatePaymentBDTO()
            {
                Amount = order.TotalPrice,
                OrderId = orderId,
                TransactionId = request.OrderID
            };
            await paymentService.CreatePaymentAsync(payment);

            //===========(3) add to shipping table ===========
            var ship = new AddOrUpdateShippingBDTO()
            {
                OrderId = orderId,
                ShippingCost = shipCost,
                ShippingAddress = shipAddress

            };
            shippingService.CreateShippingAsync(ship);



            // Optionally store the transaction details in your database
            return Ok(new { message = "Payment captured successfully" });
        }

        return BadRequest(new { message = "Payment capture failed" });
    }
    //**********************************************************
    [HttpPut("update-order-status")]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, string newStatus)
    {
        var order = await orderService.GetOrderByIdAsync(orderId);

        if (order == null || order.IsDeleted == true)
        {
            return NotFound("Order not found.");
        }

        if (newStatus == "pending") order.CurrentStatus = ModelsB.Order_B.Status.Pending;
        else if (newStatus == "shipped") order.CurrentStatus = ModelsB.Order_B.Status.Shipped;
        else if (newStatus == "delivered") order.CurrentStatus = ModelsB.Order_B.Status.Delivered;
        else if (newStatus == "cancelled") order.CurrentStatus = ModelsB.Order_B.Status.Cancelled;

        var updateOrder = mapper.Map<AddOrUpdateOrderBDTO>(order);
        await orderService.UpdateOrderAsync(updateOrder);

        return Ok(new { message = "status updated!" });
    }
}


public class PayPalOrderRequest
{
    public string OrderID { get; set; }
}
