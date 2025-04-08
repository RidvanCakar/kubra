using Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Models;
using StackExchange.Redis;
using System.Text.Json;

namespace Services
{
    public class ProductService
    {
        private readonly eCommerceContext _context;

        public ProductService(eCommerceContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.User)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Username = p.User.Username,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Stock = p.Stock,
                CategoryName = p.Category.Name


            }).ToListAsync();


            return products;

        }

        public async Task<ProductDto> GetProductAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.User)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Username = p.User.Username,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Stock = p.Stock,
                    CategoryName = p.Category.Name


                }).FirstOrDefaultAsync();

                return product;

        }

        public async Task<List<Product>> GetProductAdminAsync(int UserId)
        {
            var adminProducts = await _context.Products
            .Where(p => p.UserId == UserId)
            .ToListAsync();
            if (adminProducts == null || !adminProducts.Any())
            {
                return null;
            }

            return adminProducts;
        }

        public async Task<Product> AddProductAsync([FromBody] Product product)
        {
            var lastProduct = await _context.Products.OrderByDescending(p => p.Id).FirstOrDefaultAsync();
            if (lastProduct == null)
            {
                product.Id = 1;
            }
            else
            {
                product.Id = lastProduct.Id + 1;
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return lastProduct;
        }

        public async Task<Product> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(int id, Product updateProduct)
        {
            if (id != updateProduct.Id)
            {
                return null;
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            product.Name = updateProduct.Name;
            product.Price = updateProduct.Price;
            product.CategoryId = updateProduct.CategoryId;
            product.ImageUrl = updateProduct.ImageUrl;
            product.Stock = updateProduct.Stock;

            await _context.SaveChangesAsync();
            return product;
        }

    }
}