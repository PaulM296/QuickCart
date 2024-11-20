namespace QuickCart.Domain.Entities.OrderAggregates
{
    public enum OrderStatus
    {
        Pending,
        PaymentReceived,
        PaymentFailed
    }
}
