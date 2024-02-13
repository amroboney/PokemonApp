using System;
using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
	public interface IPokemonRepositoroy
	{
		ICollection<Pokemon> GetPokemons();
		Pokemon GetPokemon(int pokeId);
		Pokemon GetPokemon(string name);
		decimal GetRating(int pokeId);
		bool PokemonExists(int pokeId);
		bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon);
		bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon);
		bool Save();
	}
}

