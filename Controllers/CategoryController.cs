using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Data.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class CategoryController :Controller
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;
		public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
		{
			_categoryRepository = categoryRepository;
			_mapper = mapper;
		}

		//Get all categories
		[HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
		public IActionResult GetCategories()
		{
			var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
				return BadRequest(ModelState);

            return Ok(categories);
		}

		//Find categories
		[HttpGet("{catId}")]
		[ProducesResponseType(200, Type = typeof(Category))]
		[ProducesResponseType(400)]
		public IActionResult GetCategory(int catId)
		{
			if (!_categoryRepository.CategoryExists(catId))
				return NotFound();

			var category = _categoryRepository.GetCategory(catId);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(category);


        }


		// Get pokemon by category id
		[HttpGet("pokemon/{catId}")]
		[ProducesResponseType(200, Type = typeof(Pokemon))]
		[ProducesResponseType(400)]
		public IActionResult GetPokemonByCategoryId(int catId)
		{
			var pokemons = _categoryRepository.GetPokemonByCatoryId(catId);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(pokemons);
		}

		// Save the category
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
		{
			
			if (categoryCreate == null)
				return BadRequest(ModelState);

			var category = _categoryRepository.GetCategories()
				.Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper())
				.FirstOrDefault();

			if(category != null)
			{
				ModelState.AddModelError("", "Category allrady exists");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest();

			var categoryMap = _mapper.Map<Category>(categoryCreate);

			if (!_categoryRepository.CreateCategory(categoryMap)){
				ModelState.AddModelError("", "Something went rong on save data");
				return StatusCode(500, ModelState);
			}

			return Ok("succefly saved category");

		}

		// update category
		[HttpPut("{categoryId}")]
		[ProducesResponseType(500)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
        public IActionResult updateCategory(int categoryId, [FromBody] CategoryDto updateCategory)
		{
			if (updateCategory == null)
				return BadRequest(ModelState);

			if (categoryId != updateCategory.Id)
				return BadRequest(ModelState);

			if (!_categoryRepository.CategoryExists(categoryId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var categoryMap = _mapper.Map<Category>(updateCategory);

			if (!_categoryRepository.UpdateCategory(categoryMap))
			{
				ModelState.AddModelError("", "somthing went when save data");
				return StatusCode(500, ModelState);
			}

			return NoContent() ;
		}


		[HttpDelete("{categoryId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(402)]
		[ProducesResponseType(204)]
		public IActionResult DeleteCategory(int categoryId)
		{
			if (!_categoryRepository.CategoryExists(categoryId))
				return NotFound();

			var categoryToDelete = _categoryRepository.GetCategory(categoryId);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if(!_categoryRepository.DeleteCategory(categoryToDelete))
			{
				ModelState.AddModelError("", "something is rong when delete category");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}
	}
}

