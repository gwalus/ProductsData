using ShoesApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoesApp.Data
{
    public interface IDataRepository
    {
        Task<List<Product>> GetProducts();
        Task<bool> Add<T>(T entity) where T : class;
        Task<bool> Update(Product productUpdated);
        Task<List<Product>> SearchProduct(string name);
    }
}
