using System;
using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
	public interface IOwnerRepository
	{
		ICollection<Owner> GetOwners();

		Owner GetOwner(int id);

		ICollection<Owner> GetOwnerOfAPokemon(int pokeId);

		ICollection<Pokemon> GetPokemonsByOwner(int ownerId);

		bool OwnerExists(int id);

		bool createOwner(Owner owner);

		bool Save();
	}
}

