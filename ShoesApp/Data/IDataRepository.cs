using ShoesApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoesApp.Data
{
    public interface IDataRepository
    {
        public Task<List<Product>> GetProducts();
        public Task<bool> Add<T>(T entity) where T : class;
        public Task<bool> Update(Product productUpdated);
    }
}
