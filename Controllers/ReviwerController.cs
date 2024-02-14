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

	public class ReviwerController: Controller
	{
		private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviwerController(IReviewerRepository reviewerRepository, IMapper mapper)
		{
			_reviewerRepository = reviewerRepository;
            _mapper = mapper;
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
        [HttpGet("{pokeId}/review")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewByReviewer(int reviewId)
        {

            var reviews = _reviewerRepository.GetReviewsByReviewer(reviewId);

            if (!ModelState.IsValid)
                return BadRequest();


            return Ok(reviews);
        }

        //  Save Reviewer
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerCreate)
        {
            if (reviewerCreate == null)
                return BadRequest(ModelState);

            var reviewer = _reviewerRepository.GetReviewers()
                .Where(c => c.LastName.Trim().ToUpper() == reviewerCreate.LastName.Trim().ToUpper())
                .FirstOrDefault();

            if (reviewer != null)
            {
                ModelState.AddModelError("", "reviewer allready exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);

            if (!_reviewerRepository.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "somthing went rong on save country");
                return StatusCode(500, ModelState);
            }

            return Ok("successflu saved Reviewer");
        }


        // Delete Reviewwe
        [HttpDelete("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(402)]
        [ProducesResponseType(204)]
        public IActionResult DeleteReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            var reviewerToDelete = _reviewerRepository.GetReviewer(reviewerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewerRepository.DeleteReviewer(reviewerToDelete))
            {
                ModelState.AddModelError("", "something is rong when delete reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

