using System;
using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
	public interface ICategoryRepository
	{
		ICollection<Category> GetCategories();

		Category GetCategory(int catId);

		ICollection<Pokemon> GetPokemonByCatoryId(int categoryId);

		bool CategoryExists(int catId);

		bool CreateCategory(Category category);

		bool Save();
	}
}

