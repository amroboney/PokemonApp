using System;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository
{
	public class PokemonRepository: IPokemonRepositoroy
	{
		private readonly DataContext _context;

		public PokemonRepository(DataContext context)
		{
			_context = context;
		}

        public ICollection<Pokemon> GetPokemons()
        {
			return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }
    }
}

