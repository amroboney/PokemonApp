using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Data.Dto;
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
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IPokemonRepositoroy _pokemonRepositoroy;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository,IReviewerRepository reviewerRepository, IPokemonRepositoroy pokemonRepositoroy, IMapper mapper)
		{
			_reviewRepository = reviewRepository;
            _reviewerRepository = reviewerRepository;
            _pokemonRepositoroy = pokemonRepositoroy;
            _mapper = mapper;
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

        //  Save review
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokemonId, [FromBody] ReviewDto reviewCreate)
        {

            if (reviewCreate == null)
                return BadRequest(ModelState);

            var owner = _reviewRepository.GetReviews()
                .Where(c => c.Title.Trim().ToUpper() == reviewCreate.Title.Trim().ToUpper())
                .FirstOrDefault();

            if (owner != null)
            {
                ModelState.AddModelError("", "Review allready exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewMap = _mapper.Map<Review>(reviewCreate);

            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);
            reviewMap.Pokemon = _pokemonRepositoroy.GetPokemon(pokemonId);


            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "somthing went rong on save country");
                return StatusCode(500, ModelState);
            }

            return Ok("successflu saved Review");
        }


    }
}

