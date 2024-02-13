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

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon,
            };

            _context.Add(pokemonCategory);


            _context.Add(pokemon);
            return Save();
        }

        public Pokemon GetPokemon(int pokeId)
        {
            return _context.Pokemons.Where(p => p.Id == pokeId).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemons()
        {
			return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public decimal GetRating(int pokeId)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);

            if (review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemons.Any(p => p.Id == pokeId); 
        }

        public bool Save()
        {
            var IsSave = _context.SaveChanges();
            return IsSave > 0 ? true : false;
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            throw new NotImplementedException();
        }
    }
}

