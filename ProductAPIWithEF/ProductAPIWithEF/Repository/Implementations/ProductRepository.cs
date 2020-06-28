using Microsoft.EntityFrameworkCore;
using ProductAPIWithEF.Data;
using ProductAPIWithEF.Models;
using ProductAPIWithEF.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPIWithEF.Repository.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<Product[]> Get()
        {
            try
            {
                IQueryable<Product> query = _context.Products
               .Include(p => p.Category);

                query = query.AsNoTracking().OrderBy(o => o.Id);

                return await query.ToArrayAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Product[]> GetByCategory(int category_id)
        {
            try
            {
                IQueryable<Product> query = (IQueryable<Product>)_context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(x => x.CategoryId == category_id);

                return await query.ToArrayAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> GetById(int id)
        {
            try
            {
                IQueryable<Product> query = _context.Products
                .Include(p => p.Category);

                query = query.AsNoTracking().OrderBy(o => o.Id);

                return await query.FirstOrDefaultAsync(p => p.Id == id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Product Add(Product entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entity;
        }

        public Product Update(Product entity)
        {
            var result = _context.Products.SingleOrDefault(p => p.Id.Equals(entity.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(entity);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return entity;
        }

        public void Delete(int id) 
        {
            var result = _context.Products.SingleOrDefault(p => p.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    _context.Remove(result);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
