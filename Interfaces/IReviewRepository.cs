using System;
using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
	public interface IReviewRepository
	{
		ICollection<Review> GetReviews();

		Review GetReview(int id);

		ICollection<Review> GetReviewOfAPokemon(int pokeId);

		bool ReviewExists(int id);

		bool CreateReview(Review review);

		bool Save();
	}
}

