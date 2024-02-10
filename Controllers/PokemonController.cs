using System;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class PokemonController: Controller
	{
		private readonly IPokemonRepositoroy _pokemonRepository;

		public PokemonController(IPokemonRepositoroy pokemonRepositoroy)
		{
			_pokemonRepository = pokemonRepositoroy;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]

		public IActionResult GetPokemons()
		{
			var pokemons = _pokemonRepository.GetPokemons();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(pokemons);
		}
	}
}

