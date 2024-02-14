using System;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository
{
	public class ReviewerRepository: IReviewerRepository
	{
        private DataContext _context;
		public ReviewerRepository(DataContext context)
		{
            _context = context;
		}

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _context.Remove(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int id)
        {
            return _context.Reviewers.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.OrderBy(r => r.Id).ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int id)
        {
            return _context.Reviewers.Any(r => r.Id == id);
        }

        public bool Save()
        {
            var IsSave = _context.SaveChanges();
            return IsSave > 0 ? true : false;
        }
    }
}

