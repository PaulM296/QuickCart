using QuickCart.Domain.Entities;

namespace QuickCart.Domain.Specifications
{
    public class BrandListSpecification : BaseSpecification<Product, string>
    {
        public BrandListSpecification()
        {
            AddSelect(x => x.Brand);
            ApplyDistinct();
        }

    }
}
