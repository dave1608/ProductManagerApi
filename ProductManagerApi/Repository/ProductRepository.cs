using Microsoft.EntityFrameworkCore;
using ProductManagerApi.Data;
using ProductManagerApi.Models;

namespace ProductManagerApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ProductModel> GetByIdAsync(int id)
        {
            return await _appDbContext.Products
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProductModel> GetByNameAsync(string name)
        {
            return await _appDbContext.Products
                .FirstOrDefaultAsync(x => x.Name.ToUpper() == name.ToUpper());
        }

        public async Task<List<ProductModel>> GetAllAsync()
        {
            return await _appDbContext.Products.ToListAsync();
        }

        public async Task<ProductModel> AddAsync(ProductModel product)
        {
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<ProductModel> UpdateAsync(ProductModel product)
        {
            var productDb = await GetByIdAsync(product.Id);
            if (productDb == null)
                throw new Exception("Product not found");
            productDb.Name = product.Name;
            productDb.Departament = product.Departament;
            productDb.Price = product.Price;
            productDb.Quantity = product.Quantity;
            productDb.MinimumQuantity = product.MinimumQuantity;
            productDb.UpdatedAt = DateTime.UtcNow;
            _appDbContext.Products.Update(productDb);
            await _appDbContext.SaveChangesAsync();
            return productDb;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var productDb = await GetByIdAsync(id);
            if (productDb == null)
                throw new Exception("Product not found");
            _appDbContext.Products.Remove(productDb);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
