
using CRUD.Models;

namespace CRUD.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(int pagenumber, int pagesize);
        Task<Product> GetProductByIdAsync(int productId);
        Task<Product> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsyc(int id, Product product);
        Task<bool> DeleteProductAsyc(int id);
    }
}
