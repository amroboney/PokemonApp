using System;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Repository;

namespace PokemonApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class OwnerController: Controller
	{
		private readonly IOwnerRepository _ownerRepository;

		public OwnerController(IOwnerRepository ownerRepository)
		{
			_ownerRepository = ownerRepository;
		}

        //Get Owner
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwners()
        {
            var countries = _ownerRepository.GetOwners();

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(countries);
        }

        //Find owner
        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult getOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            var owner = _ownerRepository.GetOwner(ownerId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(owner);
        }

        // Get
        [HttpGet("{ownerId}/owner")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            var pokemons = _ownerRepository.GetPokemonsByOwner(ownerId);

            if (!ModelState.IsValid)
                return BadRequest();


            return Ok(pokemons );
        }



        // Get
        [HttpGet("{pokeId}/GetOwnerOfAPokemon")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnerOfAPokemon(int ownerId)
        {
            var country = _ownerRepository.GetOwnerOfAPokemon(ownerId);

            if (!ModelState.IsValid)
                return BadRequest();


            return Ok(country);
        }
    }
}

