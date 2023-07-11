using Api.Entities;
using API.Data;
using API.DTO;
using API.Errors;
using API.Interfaces;
using API.Interfaces.Implaments;
using API.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class PruductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _genericProduct;
        private readonly IGenericRepository<ProductType> _genericType;
        private readonly IGenericRepository<ProductBrand> _genericBrand;
        private readonly IMapper _mapper;

        public PruductsController
            (
            IGenericRepository<Product> genericProduct, 
            IGenericRepository<ProductBrand> genericBrand,
            IGenericRepository<ProductType> genericType,
            IMapper mapper
            ) 
        {
           _genericProduct = genericProduct;
            _genericBrand = genericBrand;
            _genericType = genericType;
            _mapper = mapper;
        }
        [HttpGet] 
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts() 
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var product =  await _genericProduct.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(product));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct( int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product =  await _genericProduct.GetEntityWithSpec(spec);
            if (product == null) return NotFound(new ApiResponse (404));
            return _mapper.Map<Product ,ProductToReturnDto>(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _genericBrand.ListAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _genericType.ListAllAsync());
        }
    }
}
