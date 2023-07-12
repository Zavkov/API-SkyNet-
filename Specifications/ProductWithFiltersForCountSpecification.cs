using Api.Entities;

namespace API.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams Productparams): base 
            (x =>
            (string.IsNullOrEmpty(Productparams.Search) || x.Name.ToLower().Contains(Productparams.Search)) &&
            (!Productparams.BrandId.HasValue || x.ProductBrandId == Productparams.BrandId)
            && (!Productparams.TypeId.HasValue || x.ProductTypeId == Productparams.TypeId))
        {    
        }
    }
}
