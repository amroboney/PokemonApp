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
	}
}

