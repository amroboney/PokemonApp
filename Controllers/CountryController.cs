using System;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class CountryController : Controller
	{
		private readonly ICountryRepository _countryRepository;

		public CountryController(ICountryRepository countryRepository)
		{
			_countryRepository = countryRepository;
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

    }
}

