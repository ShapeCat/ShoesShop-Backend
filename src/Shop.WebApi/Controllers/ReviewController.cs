using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Create review
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Review/
        ///     {
        ///         "modelId": "5c6f2bc5-8168-44ff-9ee9-18526325d923",
        ///         "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "rating": 5,
        ///         "comment": "Some comment",
        ///         "publishDate": "2023-12-12T00:04:35.235Z"
        ///     }
        ///
        /// Return: ID of created review
        /// </remarks>
        /// <param name="reviewDto">Review</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="404">Model with given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([FromBody] ReviewDto reviewDto)
        {
            var command = Mapper.Map<CreateReviewCommand>(reviewDto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get all reviews
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/review
        ///     
        /// Return: List of all reviews
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAll()
        {
            var command = new GetAllReviewsQuery();
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get review by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Review/e034b7ef-03c0-4c01-aa00-55a39cbcfcd7
        /// 
        /// Return: Single review with the given Id, if exists
        /// </remarks>
        /// <param name="reviewId">Review Id</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="404">Review with the same Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("{reviewId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReviewVm>> GetById([Required] Guid reviewId)
        {
            var query = new GetReviewQuery()
            {
                ReviewId = reviewId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Delete review
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Review/5c6f2bc5-8168-44ff-9ee9-18526325d923
        /// 
        /// </remarks>
        /// <param name="reviewId">Review Id</param>
        /// <response code="204">Successful Operation</response>
        /// <response code="404">Review with the given Id not found</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpDelete("{reviewId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] Guid reviewId)
        {
            var command = new DeleteReviewCommand()
            {
                ReviewId = reviewId
            };
            await Mediator.Send(command);
            return Ok();
        }
    }
}
