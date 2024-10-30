using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickCart.Domain.Entities;
using QuickCart.Domain.Interfaces;

namespace QuickCart.Api.Controllers
{
    public class PaymentsController(IPaymentService paymentService,
        IBaseRepository<DeliveryMethod> dmRepo) : BaseApiController
    {
        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<ActionResult<ShoppingCart>> CreateOrUpdatePaymentIntent(string cartId)
        {
            var cart = await paymentService.CreateOrUpdatePaymentIntent(cartId);

            if (cart == null)
                return BadRequest("Problem with your cart.");

            return Ok(cart);
        }

        [HttpGet("delivery-methods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await dmRepo.ListAllAsync());
        }
    }
}
