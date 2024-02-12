using System;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Repository;

namespace PokemonApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class ReviwerController: Controller
	{
		private readonly IReviewerRepository _reviewerRepository;

		public ReviwerController(IReviewerRepository reviewerRepository)
		{
			_reviewerRepository = reviewerRepository;
		}


        //Get Reviwers
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public IActionResult GetReviewers()
        {
            var reviewers = _reviewerRepository.GetReviewers();

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviewers);
        }

        //Find reviewer
        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (! _reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            var review = _reviewerRepository.GetReviewer(reviewerId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(review);
        }

        // Get Reviewer of the review
        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewByReviewer(int reviewId)
        {

            var reviews = _reviewerRepository.GetReviewsByReviewer(reviewId);

            if (!ModelState.IsValid)
                return BadRequest();


            return Ok(reviews);
        }
    }
}

