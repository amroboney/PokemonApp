using System;
using System.Diagnostics.Metrics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Data.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class CountryController : Controller
	{
		private readonly ICountryRepository _countryRepository;
		private readonly IMapper _mapper;
		public CountryController(ICountryRepository countryRepository, IMapper mapper)
		{
			_countryRepository = countryRepository;
			_mapper = mapper;
		}


		//Get Countries
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
		public IActionResult GetCountries()
		{
			var countries = _countryRepository.GetCountries();

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(countries);
		}

		//Find country
		[HttpGet("{countryId}")]
		[ProducesResponseType(200, Type = typeof(Country))]
		[ProducesResponseType(400)]
		public IActionResult getCountry(int countryId)
		{
			if (!_countryRepository.CountryExists(countryId))
				return NotFound();

			var country = _countryRepository.GetCountry(countryId);

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(country);
		}

		// Get country by Owner
		[HttpGet("/owner/{ownerId}")]
		[ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult  GetCountoryByOwner(int ownerId)
        {
			var country = _countryRepository.GetCountryByOwner(ownerId);

			if (!ModelState.IsValid)
				return BadRequest();
			 

			return Ok(country);
        }


		//  Save countyr
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
		{
			if (countryCreate == null)
				return BadRequest(ModelState);

			var country = _countryRepository.GetCountries()
				.Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.Trim().ToUpper())
				.FirstOrDefault();

			if(country != null )
			{
				ModelState.AddModelError("", "Country allready exists");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest();

			var countryMap = _mapper.Map<Country>(countryCreate);

			if (!_countryRepository.CreateCountry(countryMap))
			{
				ModelState.AddModelError("", "somthing went rong on save country");
				return StatusCode(500, ModelState);
			}

			return Ok("successflu saved country");
		}
		

    }
}

