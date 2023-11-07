using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class AdressController : AbstractController
    {
        public AdressController(IMapper mapper) : base(mapper) { }

        /// <summary>
        /// Create adress
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Adress/4a162ded-1cff-4603-b217-e9fdbebc7f37
        ///     {
        ///         "colorName": "Black",
        ///         "skuID": "FF1234567890",
        ///         "releaseDate": "2021-10-17T21:44:16.871Z"
        ///     }
        /// 
        /// Creates adress
        /// Returns ID of created adress
        /// </remarks>
        /// <returns>ID of created adress</returns>
        /// <param name="adressDto">Adress creation information</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([FromBody] AdressDto adressDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (adressDto is null) return BadRequest(ModelState);

            var command = mapper.Map<CreateAdressCommand>(adressDto);
            var result = await Mediator.Send(command);
            return Ok(result);


        }

        /// <summary>
        /// Return list of all adresses
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Adress
        ///     
        /// Returns list of all adresses
        /// </remarks>
        /// <returns>List of all adresses</returns>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AdressVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AdressVm>>> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var query = new GetAllAdressesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Find adress by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Adress/e034b7ef-03c0-4c01-aa00-55a39cbcfcd7
        /// 
        /// Returns single adress with the same ID, if exists
        /// </remarks>
        /// <returns>Single adress with the same ID, if exists</returns>
        /// <param name="adressId">Adress ID</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="404">Adress with the same ID not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("{adressId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdressVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AdressVm>> GetById(Guid adressId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var query = new GetAdressQuery()
                {
                    AdressId = adressId
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
        /// Update adress information by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Adress/5da752ea-3a18-4bd9-aaca-66519c23a049
        ///     {
        ///         "colorName": "White",
        ///         "skuID": "4356765546865",
        ///         "releaseDate": "1990-10-17T21:44:16.871Z"
        ///     }
        /// 
        /// Updates Adress with the same ID, if exists
        /// </remarks>
        /// <param name="adressId">Adress ID</param>
        /// <param name="descriptionDto">New adress information</param>
        /// <response code="204">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="404">adress with the same ID not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPut("{adressId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateDescription(Guid adressId, [FromBody] AdressDto descriptionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (descriptionDto is null) return BadRequest(ModelState);
            try
            {
                var command = mapper.Map<UpdateAdressCommand>(descriptionDto);
                command.AdressId = adressId;
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Delete adress by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Adress/5c6f2bc5-8168-44ff-9ee9-18526325d923
        /// 
        /// Deletes adress with the same ID, if exists
        /// </remarks>
        /// <param name="adressId">Adress ID</param>
        /// <response code="204">Successful Operation</response>
        /// <response code="404">Adress with the same ID not found</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpDelete("{adressId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid adressId)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var command = new DeleteAdressCommand()
                {
                    AdressId = adressId
                };
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
