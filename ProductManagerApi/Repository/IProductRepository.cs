using ProductManagerApi.Models;

namespace ProductManagerApi.Repository
{
    public interface IProductRepository
    {
        Task<ProductModel> GetByIdAsync(int id);
        Task<ProductModel> GetByNameAsync(string name);
        Task<List<ProductModel>> GetAllAsync();
        Task<ProductModel> AddAsync(ProductModel product);
        Task<ProductModel> UpdateAsync(ProductModel product);
        Task<bool> DeleteAsync(int id);
    }
}
