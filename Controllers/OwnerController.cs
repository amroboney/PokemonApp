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

	public class OwnerController: Controller
	{
		private readonly IOwnerRepository _ownerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
		public OwnerController(IOwnerRepository ownerRepository, ICountryRepository countryRepository, IMapper mapper)
		{
			_ownerRepository = ownerRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
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


        //  Save Owner
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwener([FromQuery] int countryId, [FromBody] OwnerDto ownerCreate)
        {

            if (ownerCreate == null)
                return BadRequest(ModelState);

            var owner = _ownerRepository.GetOwners()
                .Where(c => c.LastName.Trim().ToUpper() == ownerCreate.LastName.Trim().ToUpper())
                .FirstOrDefault();

            if (owner != null)
            {
                ModelState.AddModelError("", "Owner allready exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            var ownerMap = _mapper.Map<Owner>(ownerCreate);

            ownerMap.Country = _countryRepository.GetCountry(countryId);

            if (!_ownerRepository.createOwner(ownerMap))
            {
                ModelState.AddModelError("", "somthing went rong on save country");
                return StatusCode(500, ModelState);
            }

            return Ok("successflu saved Owners");
        }
    }
}

