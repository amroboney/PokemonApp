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
	public class PokemonController : Controller
	{
		private readonly IPokemonRepositoroy _pokemonRepository;
		private readonly IMapper _mapper;

		public PokemonController(IPokemonRepositoroy pokemonRepositoroy, IMapper mapper)
		{
			_pokemonRepository = pokemonRepositoroy;
			_mapper = mapper;
		}

		// Get all pokemon
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
		public IActionResult GetPokemons()
		{
			//var pokemons = _pokemonRepository.GetPokemons();
			var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());


            if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(pokemons);
		}

		// find the pokemon
		[HttpGet("{bokeId}")]
		[ProducesResponseType(200, Type=typeof(Pokemon))]
		[ProducesResponseType(400)]
		public IActionResult GetBokemon(int pokeId)
		{
			if (!_pokemonRepository.PokemonExists(pokeId))
				return NotFound();

			var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(pokeId));
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(pokemon);
		}

		//get the rating for the pokemon
        [HttpGet("{bokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonReting(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();

            var rating = _pokemonRepository.GetRating(pokeId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rating);
        }

    }
}

