using Microsoft.Extensions.Configuration;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using System.Threading.Tasks;
using DTOsB.Order.OrderDTO;


namespace ApplicationB.Services_B.Order
{

    public class PayPalService
    {
        private readonly PayPalHttpClient _client;

        public PayPalService(IConfiguration configuration)
        {
            // Use either SandboxEnvironment or LiveEnvironment based on configuration
            var environment = new SandboxEnvironment(
                configuration["PayPal:ClientId"],
                configuration["PayPal:Secret"]);
            _client = new PayPalHttpClient(environment);
        }

        public async Task<SelectOrderBDTO> CaptureOrderAsync(string orderId)
        {
            var request = new OrdersCaptureRequest(orderId);
            request.RequestBody(new OrderActionRequest());

            try
            {
                var response = await _client.Execute(request);
                return response.Result<SelectOrderBDTO>();
            }
            catch (HttpException ex)
            {
                // Handle error response
                Console.WriteLine($"PayPal capture failed: {ex.Message}");
                throw;
            }
        }
    }
}
