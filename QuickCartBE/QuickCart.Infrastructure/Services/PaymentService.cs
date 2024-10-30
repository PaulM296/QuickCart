using QuickCart.Domain.Entities;
using QuickCart.Domain.Interfaces;

namespace QuickCart.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<ShoppingCart> CreateOrUpdatePaymentIntent(string cardId)
        {
            throw new NotImplementedException();
        }
    }
}
