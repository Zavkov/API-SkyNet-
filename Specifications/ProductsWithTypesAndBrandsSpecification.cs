using Api.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace API.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams Productparams) :
            base ( x => (string.IsNullOrEmpty(Productparams.Search) || x.Name.ToLower().Contains(Productparams.Search)) &&
            (!Productparams.BrandId.HasValue || x.ProductBrandId == Productparams.BrandId)
            && (!Productparams.TypeId.HasValue || x.ProductTypeId == Productparams.TypeId))
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(Productparams.PageSize * (Productparams.PageIndex - 1), Productparams.PageSize);

            if (!string.IsNullOrEmpty(Productparams.Sort))
            {
                switch (Productparams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price); break;
                    case "priceDesc":
                        AddOrderByDescending(p=>p.Description); break;
                    default:
                        AddOrderBy(n => n.Name); break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x=>x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
