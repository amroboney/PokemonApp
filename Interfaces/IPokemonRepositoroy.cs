using System;
using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
	public interface IPokemonRepositoroy
	{
		ICollection<Pokemon> GetPokemons();
	}
}

