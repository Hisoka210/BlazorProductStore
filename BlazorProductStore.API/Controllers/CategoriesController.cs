using BlazorProductStore.BLL; // You would create a CategoryService similar to ProductService
using BlazorProductStore.DAL.Models; // You would create a Category model
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _categoryService; // Assume you created this service

    public CategoriesController(IConfiguration configuration)
    {
        // For simplicity, instantiating directly. Use DI in a real app.
        var connString = configuration.GetConnectionString("DefaultConnection");
        _categoryService = new CategoryService(connString);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Category>> Get()
    {
        return Ok(_categoryService.GetCategories());
    }

    // Implement POST, PUT, DELETE actions
}