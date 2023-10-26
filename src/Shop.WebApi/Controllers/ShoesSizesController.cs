using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.WebApi.Dto;

namespace ShoesShop.WebAPI.Controllers
{
    public class ShoesSizesController : AbstractController
    {
        public ShoesSizesController(IMapper mapper) : base(mapper) { }

        /// <summary>
        /// Return list of all shoes sizes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/ShoesSizes
        ///     
        /// Returns list of all shoes sizes
        /// </remarks>
        /// <returns>List of all shoes sizes</returns>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ShoesSizeVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ShoesSizeVm>>> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var query = new GetAllShoesSizesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Find shoes size by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/ShoesSizes/b39faddb-8ad5-49d8-8443-1337260bd47f
        /// 
        /// Returns single shoes size with the same ID, if exists
        /// </remarks>
        /// <returns>Single shoes size with the same ID, if exists</returns>
        /// <param name="shoesSizeId">Shoes size ID</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="404">Shoes size with the same ID not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("{shoesSizeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoesSizeVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ShoesSizeVm>> GetShoesSize(Guid shoesSizeId)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var query = new GetShoesSizeQuery()
                {
                    ShoesSizeId = shoesSizeId
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
        /// Create shoes size for shoes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/ShoesSize/afb924c5-7b39-4cd2-b5ec-4b966102f4dc     	
        ///     {
        ///       "size": 40,
        ///       "price": 100,
        ///       "itemsLeft": 4
        ///     }
        /// 
        /// Creates shoes size for shoes with the same ID, if not exists
        /// Returns ID of created shoes size
        /// </remarks>
        /// <returns>ID of created shoes size</returns>
        /// <param name="shoesId">Shoes ID</param>
        /// <param name="shoesSizeDto">Shoes size creation information</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="404">Shoes with the same ID not found</response>
        /// <response code="409">Same shoes size aready exists</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPost("{shoesId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> CreateShoesSize(Guid shoesId, [FromBody] ShoesSizeDto shoesSizeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (shoesSizeDto is null) return BadRequest(ModelState);
            try
            {
                var command = mapper.Map<CreateShoesSizeCommand>(shoesSizeDto);
                command.ShoesId = shoesId;
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (AlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Delete shoes size by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/ShoesSize/0b2b6efe-0609-48ec-b5cc-09508fc7b4a1
        /// 
        /// Deletes shoes size with the same ID, if exists
        /// </remarks>
        /// <param name="shoesSizeId">Shoes size ID</param>
        /// <response code="204">Successful Operation</response>
        /// <response code="404">Shoes size with the same ID not found</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpDelete("{shoesSizeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteShoesSize(Guid shoesSizeId)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var command = new DeleteShoesSizeCommand()
                {
                    ShoesSizeId = shoesSizeId
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
        /// Updates shoes size information by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/ShoesSize/e3c1447e-2ecc-48a1-89ba-6ba502194b55
        ///     {
        ///       "size": 20,
        ///       "price": 2200,
        ///       "itemsLeft": 0
        ///     }
        /// 
        /// Updates shoes size with the same ID, if exists
        /// </remarks>
        /// <param name="shoesSizeId">Shoes size ID</param>
        /// <param name="shoesSizeDto">New shoes size information</param>
        /// <response code="204">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="404">Shoes size with the same ID not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPut("{shoesSizeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateShoes(Guid shoesSizeId, [FromBody] ShoesSizeDto shoesSizeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (shoesSizeDto is null) return BadRequest(ModelState);
            try
            {
                var command = mapper.Map<UpdateShoesSizeCommand>(shoesSizeDto);
                command.ShoesSizeId = shoesSizeId;
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