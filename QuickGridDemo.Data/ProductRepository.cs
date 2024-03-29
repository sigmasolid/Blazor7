﻿using Microsoft.EntityFrameworkCore;
using QuickGridDemo.Data.Models;

namespace QuickGridDemo.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly NorthwindContext _context;
        private IOrderedQueryable<Product>? products;

        public ProductRepository(NorthwindContext context)
        {
            _context = context;
        }

        
        public IQueryable<Product> SearchProducts(string? filter)
        {
            products = _context.Products.Include(p => p.Supplier).Include(p => p.Category).AsNoTracking().OrderBy(x => x.ProductName);
            if (!string.IsNullOrEmpty(filter))
            {
                products = products.Where(x => x.ProductName.Contains(filter)).OrderBy(x => x.ProductName);
            }
            return products;
        }
    }

    public interface IProductRepository
    {
        IQueryable<Product> SearchProducts(string? filter);
    }
}