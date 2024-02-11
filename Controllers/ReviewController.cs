using System;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Repository;

namespace PokemonApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class ReviewController: Controller
	{
		private readonly IReviewRepository _reviewRepository;

		public ReviewController(IReviewRepository reviewRepository)
		{
			_reviewRepository = reviewRepository;
		}

        //Get Reviws
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _reviewRepository.GetReviews();

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        //Find review
        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (! _reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var review = _reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(review);
        }

        // Get Review of the specific pokemon
        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewForPokemon(int pokeId)
        {

            var reviews = _reviewRepository.GetReviewOfAPokemon(pokeId);

            if (!ModelState.IsValid)
                return BadRequest();


            return Ok(reviews);
        }



        
    }
}

