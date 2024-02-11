﻿using System;
using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
	public interface ICountryRepository
	{
		ICollection<Country> GetCountries();

		Country GetCountry(int id);

		Country GetCountryByOwner(int ownerId);

		ICollection<Owner> GetOwnersFromACountry(int countryId);

		bool CountryExists(int id);
	}
}

