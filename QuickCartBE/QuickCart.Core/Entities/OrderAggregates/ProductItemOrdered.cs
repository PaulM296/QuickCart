namespace QuickCart.Domain.Entities.OrderAggregates
{
    public class ProductItemOrdered
    { 
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public required string PictureUrl { get; set; }
    }
}
