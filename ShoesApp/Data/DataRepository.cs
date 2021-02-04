using Microsoft.EntityFrameworkCore;
using ShoesApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesApp.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly DataContext _context;

        public DataRepository(DataContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<bool> Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> Update(Product productUpdated)
        {
            _context.Entry(await _context.Products.FirstOrDefaultAsync(x => x.Id == productUpdated.Id)).CurrentValues.SetValues(productUpdated);

            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<List<Product>> SearchProduct(string name)
        {
            var products = await _context.Products.ToListAsync();

            var productSearched = products
                .Where(p => p.Name.StartsWith(name, System.StringComparison.OrdinalIgnoreCase) || p.Brand.StartsWith(name, System.StringComparison.OrdinalIgnoreCase))
                .ToList();

            return productSearched;
        }
    }
}