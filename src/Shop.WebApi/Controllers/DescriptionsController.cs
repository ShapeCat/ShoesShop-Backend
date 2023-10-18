using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.WebApi.Dto;

namespace ShoesShop.WebAPI.Controllers
{
    public class DescriptionsController : ControllerAbstract
    {
        public DescriptionsController(IMapper mapper) : base(mapper) { }

        /// <summary>
        /// Return list of all descriptions
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Descriptions
        ///     
        /// Returns list of all descriptions
        /// </remarks>
        /// <returns>List of all descriptions</returns>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DescriptionVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DescriptionVm>>> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var query = new GetAllDescriptionsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Find description by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Descriptions/e034b7ef-03c0-4c01-aa00-55a39cbcfcd7
        /// 
        /// Returns single description with the same ID, if exists
        /// </remarks>
        /// <returns>Single description with the same ID, if exists</returns>
        /// <param name="descriptionId">Description ID</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="404">Description with the same ID not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("{descriptionId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DescriptionVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DescriptionVm>> GetDescription(Guid descriptionId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var query = new GetDescriptionQuery()
                {
                    DescriptionId = descriptionId
                };
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Create description for shoes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Descriptions/4a162ded-1cff-4603-b217-e9fdbebc7f37
        ///     {
        ///         "colorName": "Black",
        ///         "skuID": "FF1234567890",
        ///         "releaseDate": "2021-10-17T21:44:16.871Z"
        ///     }
        /// 
        /// Creates description for shoes with the same ID, if not exists
        /// Returns ID of created description
        /// </remarks>
        /// <returns>ID of created description</returns>
        /// <param name="shoesId">Shoes ID</param>
        /// <param name="descriptionDto">Description creation information</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="404">Shoes with the same ID not found</response>
        /// <response code="409">Description for this shoes aready exists</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPost("{shoesId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> CreateDescription(Guid shoesId, [FromBody] DescriptionDto descriptionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (descriptionDto is null) return BadRequest(ModelState);
            try
            {
                var command = mapper.Map<CreateDescriptionCommand>(descriptionDto);
                command.ShoesId = shoesId;
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (AlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Delete description by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Descriptions/5c6f2bc5-8168-44ff-9ee9-18526325d923
        /// 
        /// Deletes description with the same ID, if exists
        /// </remarks>
        /// <param name="descriptionId">Description ID</param>
        /// <response code="204">Successful Operation</response>
        /// <response code="404">Description with the same ID not found</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpDelete("{descriptionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteDescription(Guid descriptionId)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var command = new DeleteDescriptionCommand()
                {
                    DescriptionId = descriptionId
                };
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Update description information by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Descriptions/5da752ea-3a18-4bd9-aaca-66519c23a049
        ///     {
        ///         "colorName": "White",
        ///         "skuID": "4356765546865",
        ///         "releaseDate": "1990-10-17T21:44:16.871Z"
        ///     }
        /// 
        /// Updates description with the same ID, if exists
        /// </remarks>
        /// <param name="descriptionId">Description ID</param>
        /// <param name="descriptionDto">New description information</param>
        /// <response code="204">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="404">Description with the same ID not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPut("{descriptionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateDescription(Guid descriptionId, [FromBody] DescriptionDto descriptionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (descriptionDto is null) return BadRequest(ModelState);
            try
            {
                var command = mapper.Map<UpdateDescriptionCommand>(descriptionDto);
                command.DescriptionId = descriptionId;
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}