using ProductAPIWithEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPIWithEF.Business.Interfaces
{
    public interface IProductBusiness
    {
        Product Create(Product model);
        Product Update(Product model);
        void Delete(int id);
        Task<Product[]> GetAll();
        Task<Product[]> GetByCategory(int category_id);
        Task<Product> GetById(int id);
    }
}
