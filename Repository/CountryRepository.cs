using System;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository
{
	public class CountryRepository: ICountryRepository
	{
        private DataContext _context;
		public CountryRepository(DataContext context)
		{
            _context = context;
		}

        public bool CountryExists(int id)
        {
            return _context.Countries.Any(c => c.Id == id);
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.OrderBy(p => p.Id).ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(p => p.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return _context.Owners.Where(c => c.Id == ownerId).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromACountry(int countryId)
        {
            return _context.Owners.Where(o => o.Country.Id == countryId).ToList();
        }
    }
}

