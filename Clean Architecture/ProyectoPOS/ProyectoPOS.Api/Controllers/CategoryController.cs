using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Request;
using POS.Application.Interfaces;
using POS.Infraestructure.Commons.Bases.Request;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryApplication _categoryApplication;

        public CategoryController(ICategoryApplication categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListCategories([FromBody] BaseFiltersRequest filters)
        {
            var response = await _categoryApplication.ListCategories(filters);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ListSelectCategories()
        {
            var response = await _categoryApplication.ListSelectCategories();
            return Ok(response);
        }

        [HttpGet("{categoryid:Int}")]
        public async Task<IActionResult> CategoryById(int categoryid)
        {
            var response = await _categoryApplication.CategoryById(categoryid);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterCategory([FromBody] CategoryRequestDTO categoryRequestDTO)
        {
            var response = await _categoryApplication.RegisterCategory(categoryRequestDTO);
            return Ok(response);
        }

        
        [HttpPut("edit/{categoryid:Int}")]
        public async Task<IActionResult> EditCategory(int categoryId, [FromBody] CategoryRequestDTO categoryRequestDTO)
        {
            var response = await _categoryApplication.EditCategory(categoryId, categoryRequestDTO);
            return Ok(response);
        }

        [HttpPut("remove/{categoryid:Int}")]
        public async Task<IActionResult> EditCategory(int categoryId)
        {
            var response = await _categoryApplication.RemoveCategory(categoryId);
            return Ok(response);
        }
    }
}