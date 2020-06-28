using ProductAPIWithEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPIWithEF.Repository.Interfaces
{
    public interface IProductRepository
    {
        Product Add(Product entity);
        Product Update(Product entity);
        void Delete(int id);

        Task<Product[]> Get();
        Task<Product> GetById(int id);
        Task<Product[]> GetByCategory(int category_id);
    }
}
