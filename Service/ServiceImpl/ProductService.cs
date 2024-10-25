using CRUD.Models;
using CRUD.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace CRUD.Service.ServiceImpl
{
    public class ProductService:IProductService
    {
        private readonly CRUDDBContext context;
        private readonly IConfiguration configuration;

        public ProductService(CRUDDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await context.Products
            .FirstOrDefaultAsync(p => p.Productid == productId);
        }

       
        public async Task<Product> CreateProductAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return await Task.FromResult(new List<Product>());

            }

            // Calculate how many items to skip
            var skip = (pageNumber - 1) * pageSize;

            // Extract products from database based on page number and size
            return await context.Products
                .Skip(skip)   // Skip the previous records
            .Take(pageSize)   // Take only the records for the current page
                .ToListAsync();
        }

        public async Task<bool> UpdateProductAsyc(int id, Product product)
        {
            var existingproduct = await context.Products.FindAsync(id);
            if (existingproduct == null) 
            {
                return false;
            }

            existingproduct.Productname=product.Productname;
            existingproduct.Categoryid=product.Categoryid;

            context.Products.Update(existingproduct);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProductAsyc(int id)
        {
            var existingproduct=await context.Products.FindAsync(id);

            if(existingproduct == null)
            {
                return false;
            }

            context.Products.Remove(existingproduct);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
