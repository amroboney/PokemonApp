using System;
using Microsoft.AspNetCore.Http.HttpResults;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository
{
	public class OwnerRepository: IOwnerRepository
	{
        private DataContext _context;

		public OwnerRepository(DataContext context)
		{
            _context = context;
		}

        public bool createOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public Owner GetOwner(int id)
        {
            return _context.Owners.Where(o => o.Id == id).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokeId)
        {
            return _context.PokemonOwners.Where(p => p.Pokemon.Id == pokeId).Select(o => o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.OrderBy(o => o.Id).ToList();
        }

        public ICollection<Pokemon> GetPokemonsByOwner(int ownerId)
        {
            return _context.PokemonOwners.Where(p => p.Owner.Id == ownerId).Select(o => o.Pokemon).ToList();

        }

        public bool OwnerExists(int id)
        {
            return _context.Owners.Any(o => o.Id == id);
        }

        public bool Save()
        {
            var IsSave = _context.SaveChanges();
            return IsSave > 0 ? true : false;
        }
    }
}

