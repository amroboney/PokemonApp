using System;
using AutoMapper;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository
{
	public class ReviewRespository: IReviewRepository
	{
        private DataContext _context;
        private readonly IMapper _mapper;

        public ReviewRespository(DataContext context, IMapper mapper)
		{
            _context = context;
            _mapper = mapper;
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public Review GetReview(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviewOfAPokemon(int pokeId)
        {
            return _context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.OrderBy(r => r.Id).ToList();
        }

        public bool ReviewExists(int id)
        {
            return _context.Reviews.Any(r => r.Id == id);
        }

        public bool Save()
        {
            var IsSave = _context.SaveChanges();
            return IsSave > 0 ? true : false;
        }
    }
}

