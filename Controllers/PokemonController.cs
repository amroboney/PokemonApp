using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Data.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Repository;

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



        //  Save Pokemon
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int categoryId, [FromQuery] int ownerId, [FromBody] PokemonDto pokemonCreate)
        {

            if (pokemonCreate == null)
                return BadRequest(ModelState);

            var owner = _pokemonRepository.GetPokemons()
                .Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (owner != null)
            {
                ModelState.AddModelError("", "Pokemon allready exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);

            if (!_pokemonRepository.CreatePokemon(ownerId, categoryId, pokemonMap))
            {
                ModelState.AddModelError("", "somthing went rong on save country");
                return StatusCode(500, ModelState);
            }

            return Ok("successflu saved Owners");
        }

    }
}

