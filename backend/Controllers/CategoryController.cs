using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Services;


namespace Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]


    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
        _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            var categories =await _categoryService.AddCategoryAsync(category);
            if (categories == null)
            {
                return BadRequest("Kategori Eklenemedi");
            }
            return Ok(new { message = "Kategori Eklendi", categories });

        }

        [HttpGet]
        public async Task<IActionResult> GetCategory()
        {
            var categories=await _categoryService.GetCategoriesAsync();
            if (categories == null || !categories.Any())
            {
                return NotFound("Kategori Bulunamadı");
            }
            return Ok(categories);

        }

        [HttpGet("{categoryName}")]
        public async Task<IActionResult> GetProductsCategory(string categoryName)
        {
            var product=await _categoryService.GetProductsByCategoryNameAsync(categoryName);
            if (product == null || !product.Any())
            {
                return NotFound("Ürün Bulunamadı");
            }
            return Ok(product);

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categories= await _categoryService.DeleteCategoryAsync(id);
            if (categories == false)
            {
                return NotFound("Kategori Bulunamadı");
            }
            return Ok(new { message = "Kategori Silindi" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category updateCategory)
        {
            var product= await _categoryService.UpdateCategoryAsync(id, updateCategory);
            if (product == false)
            {
                return NotFound("Kategori Bulunamadı");
            }
            return Ok(new { message = "Kategori Güncellendi" });
        }


    }
}