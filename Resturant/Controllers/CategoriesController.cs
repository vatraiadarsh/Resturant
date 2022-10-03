using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures.Parameters;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Api.ModelBinders;
using Microsoft.AspNetCore.Authorization;
using Entities.Roles;

namespace Resturant.Controllers
{
    [ApiController]
    [Route("api/Categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IValidator<CategoryDto> _validator;
        private readonly IRepositoryManager _repository;


        public CategoriesController(IValidator<CategoryDto> validator, IRepositoryManager repository)
        {
            _validator = validator;
            _repository = repository;
        }


        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] CategoryParameters categoryParameters)
        {
            var categories = await _repository.Category.GetAllCategoriesAsync(categoryParameters,trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(categories.MetaData));         
            var categoryDto = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                NepaliName = c.NameInNepali,
                Description = c.Description
            }).ToList();
            return Ok(categoryDto);
        }

        [HttpGet("{id}", Name = "CategoryById")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _repository.Category.GetCategoryByIdAsync(id, trackChanges: false);
            if (category == null) return NotFound();
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                NepaliName = category.NameInNepali,
                Description = category.Description
            };
            return Ok(categoryDto);

        }

        [HttpGet("collection/{ids}", Name = "CategoryCollection")]
        public async Task<IActionResult> GetCategoryCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null) return BadRequest("Parameter ids is null");
            var categories = await _repository.Category.GetCategoryCollectionAsync(ids, trackChanges: false);
            if (categories.Count() != ids.Count())
            {
                return NotFound("Some ids are not valid");
            }
            var categoryDto = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                NepaliName = c.NameInNepali,
                Description = c.Description
            }).ToList();
            return Ok(categoryDto);
        }


        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto category)
        {
            var validationResult = await _validator.ValidateAsync(category);
            if (!validationResult.IsValid) return UnprocessableEntity(validationResult.Errors.Select(e => e.ErrorMessage));

            var categoryEntity = new Category
            {
                Name = category.Name,
                NameInNepali = category.NepaliName,
                Description = category.Description
            };
            _repository.Category.CreateCategory(categoryEntity);
            await _repository.saveAsync();

            return CreatedAtRoute("CategoryById", new { id = categoryEntity.Id }, categoryEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryDto category)
        {
            var validationResult = await _validator.ValidateAsync(category);
            if (!validationResult.IsValid) return UnprocessableEntity(validationResult.Errors.Select(e => e.ErrorMessage));

            var categoryEntity = await _repository.Category.GetCategoryByIdAsync(id, trackChanges: true);
            if (categoryEntity == null) return NotFound();

            categoryEntity.Name = category.Name;
            categoryEntity.NameInNepali = category.NepaliName;
            categoryEntity.Description = category.Description;

            _repository.Category.UpdateCategory(categoryEntity);
            await _repository.saveAsync();

            return CreatedAtRoute("CategoryById", new { id = categoryEntity.Id }, categoryEntity);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _repository.Category.GetCategoryByIdAsync(id, trackChanges: false);
            if (category == null) return NotFound();
            _repository.Category.DeleteCategory(category);
            await _repository.saveAsync();
            return NoContent();
        }

    }
}