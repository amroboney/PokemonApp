using System;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class CategoryController :Controller
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryController(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		//Get all categories
		[HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
		public IActionResult GetCategories()
		{
			var categories = _categoryRepository.GetCategories();

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
	}
}

