using Api.Entities;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Interfaces.Implaments
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;    
        public ProductRepository(AppDbContext context)
        {
            _context = context;  
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }
        public async Task<IReadOnlyList<ProductType>> GetProductTypeAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p=>p.ProductType)
                .Include(p=>p.ProductBrand)
                .FirstOrDefaultAsync(p=>p.Id == id); 
        }
        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
           return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
        }

    }
}
