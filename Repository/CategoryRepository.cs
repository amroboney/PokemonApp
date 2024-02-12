using System;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository
{
	public class CategoryRepository: ICategoryRepository
	{
		private DataContext _context;

		public CategoryRepository(DataContext context)
		{
			_context = context;
		}

        public bool CategoryExists(int catId)
        {
            return _context.Categories.Any(c => c.Id == catId);
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.Id).ToList();
        }

           public Category GetCategory(int catId)
        {
            return _context.Categories.Where(c => c.Id == catId).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCatoryId(int categoryId)
        {
            
            return _context.PokemonCategories.Where(e => e.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
        }

        public bool Save()
        {
            var isSave = _context.SaveChanges();
            return isSave > 0 ? true : false; 
        }
    }
}

