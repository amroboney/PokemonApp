using System;
using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
	public interface IReviewerRepository
	{
		ICollection<Reviewer> GetReviewers();

		Reviewer GetReviewer(int id);

		ICollection<Review> GetReviewsByReviewer(int reviewerId);

		bool ReviewerExists(int id);
	}
}

