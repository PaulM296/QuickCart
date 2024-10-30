using QuickCart.Domain.Entities;

namespace QuickCart.Domain.Interfaces
{
    public interface IPaymentService
    {
        Task<ShoppingCart> CreateOrUpdatePaymentIntent(string cardId);
    }
}
