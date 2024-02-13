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

		bool CreateOwner(Owner owner);

		bool UpdateOwner(Owner owner);


		bool Save();
	}
}

