using Microsoft.EntityFrameworkCore;
using Shopy.Core;
using Shopy.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopy.Repository.Implementation
{
    public class Product_Repos : I_CRUD_Repos<Product>
    {
        private readonly ShopyDbContext _db;

        public Product_Repos(ShopyDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _db.Products.Include(x=>x.Category).OrderBy(x=>x.ProductName).ToListAsync();
        }

        public async Task<Product> GetById(int Id)
        {
            return await _db.Products.FindAsync(Id);
        }

        public async Task AddNewItem(Product NewItem)
        {
            await _db.Products.AddAsync(NewItem);
            await Save();
        }

        public async Task UpdateItem(Product NewItem)
        {
            var prodcut = await _db.Products.FindAsync(NewItem.ProductId);
            if (prodcut != null)
            {
                prodcut.ProductName = NewItem.ProductName;
                prodcut.Price = NewItem.Price;
                prodcut.Discription = NewItem.Discription;
                prodcut.Category = NewItem.Category;
                _db.Update(prodcut);
                await Save();
            }
        }

        public async Task DeleteItem(int Id)
        {
            var prodcut = await _db.Products.FindAsync(Id);
            if (prodcut != null)
            {
                _db.Products.Remove(prodcut);
                await Save();
            }
        }

        private async Task Save()
        {
           await _db.SaveChangesAsync();
        }
    }
}
