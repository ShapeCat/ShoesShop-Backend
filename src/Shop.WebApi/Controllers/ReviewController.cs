using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Addresses.OutputVMs;
using ShoesShop.Application.Requests.Reviews.Commands;
using ShoesShop.Application.Requests.Reviews.OutputVMs;
using ShoesShop.Application.Requests.Reviews.Queries;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ReviewController : AbstractController
    {
        public ReviewController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([FromBody] ReviewDto reviewDto)
        {
            try
            {
                var command = Mapper.Map<CreateReviewCommand>(reviewDto);
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (ValidationException ex) { return BadRequest(ex.Errors); }
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAll()
        {
            try
            {
                var command = new GetAllReviewsQuery();
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (ValidationException ex) { return BadRequest(ex.Errors); }
        }

        [HttpGet("{reviewId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddressVm>> GetById(Guid reviewId)
        {
            try
            {
                var query = new GetReviewQuery()
                {
                    ReviewId = reviewId
                };
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (ValidationException ex) { return BadRequest(ex.Errors); }
        }

        [HttpDelete("{reviewId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid reviewId)
        {
            try
            {
                var command = new DeleteReviewCommand()
                {
                    ReviewId = reviewId
                };
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (ValidationException ex) { return BadRequest(ex.Errors); }
        }
    }
}
