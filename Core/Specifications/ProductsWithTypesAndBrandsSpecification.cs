using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecifications<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(string sort, int? brandId, int? typeId)
            :base(x=> 
                (!brandId.HasValue || x.ProductBrandId == brandId) &&
                (!typeId.HasValue ||x.ProductTypeId == typeId)
            )
        {
            AddInclude(x=> x.ProductBrand);
            AddInclude(x=> x.ProductType);
            AddOrderBy(x=> x.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "PriceAsc":
                    AddOrderBy(p=> p.Price);
                    break;

                    case "PriceDesc":
                    AddOrderByDescending(p=> p.Price);
                    break;

                    default:
                    AddOrderBy(n=> n.Name);
                    break;
                    
                }
            }

        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x=> x.Id ==id)
        {
            AddInclude(x=> x.ProductBrand);
            AddInclude(x=> x.ProductType);
        }
    }
}