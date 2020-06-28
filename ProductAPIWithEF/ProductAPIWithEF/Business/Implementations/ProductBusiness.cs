using ProductAPIWithEF.Business.Interfaces;
using ProductAPIWithEF.Models;
using ProductAPIWithEF.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPIWithEF.Business.Implementations
{
    public class ProductBusiness : IProductBusiness
    {
        private IProductRepository _repository;

        public ProductBusiness(IProductRepository repository)
        {
            _repository = repository;
        }

        public Product Create(Product model)
        {
            var productEntity = _repository.Add(model);
            return productEntity;
        }
        public Product Update(Product model)
        {
            var productEntity = _repository.Update(model);
            return productEntity;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public async Task<Product[]> GetAll()
        {
            return await _repository.Get();
        }

        public async Task<Product[]> GetByCategory(int category_id)
        {
            return await _repository.GetByCategory(category_id);
        }

        public async Task<Product> GetById(int id)
        {
            return await _repository.GetById(id);
        }     
    }
}
