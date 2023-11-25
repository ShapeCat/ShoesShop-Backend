using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Addresses.Commands;
using ShoesShop.Application.Requests.Addresses.OutputVMs;
using ShoesShop.Application.Requests.Addresses.Queries;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class AddressController : AbstractController
    {
        public AddressController(IMapper mapper) : base(mapper) { }

        /// <summary>
        /// Create address
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Address/4a162ded-1cff-4603-b217-e9fdbebc7f37
        ///     {
        ///         "colorName": "Black",
        ///         "skuID": "FF1234567890",
        ///         "releaseDate": "2021-10-17T21:44:16.871Z"
        ///     }
        /// 
        /// Creates address
        /// Returns ID of created address
        /// </remarks>
        /// <returns>ID of created address</returns>
        /// <param name="addressDto">Address creation information</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([FromBody] AddressDto addressDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (addressDto is null) return BadRequest(ModelState);

            var command = Mapper.Map<CreateAddressCommand>(addressDto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Return list of all addresses
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Address
        ///     
        /// Returns list of all addresses
        /// </remarks>
        /// <returns>List of all addresses</returns>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AddressVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AddressVm>>> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var query = new GetAllAddressesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Find address by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Address/e034b7ef-03c0-4c01-aa00-55a39cbcfcd7
        /// 
        /// Returns single address with the same ID, if exists
        /// </remarks>
        /// <returns>Single address with the same ID, if exists</returns>
        /// <param name="addressId">Address ID</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="404">Address with the same ID not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("{addressId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddressVm>> GetById(Guid addressId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var query = new GetAddressQuery()
                {
                    AddressId = addressId
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
        /// Update address information by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Address/5da752ea-3a18-4bd9-aaca-66519c23a049
        ///     {
        ///         "colorName": "White",
        ///         "skuID": "4356765546865",
        ///         "releaseDate": "1990-10-17T21:44:16.871Z"
        ///     }
        /// 
        /// Updates Address with the same ID, if exists
        /// </remarks>
        /// <param name="addressId">Address ID</param>
        /// <param name="descriptionDto">New address information</param>
        /// <response code="204">Successful Operation</response>
        /// <response code="400">Invalid request</response>
        /// <response code="404">address with the same ID not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPut("{addressId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(Guid addressId, [FromBody] AddressDto descriptionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (descriptionDto is null) return BadRequest(ModelState);
            try
            {
                var command = Mapper.Map<UpdateAddressCommand>(descriptionDto);
                command.AddressId = addressId;
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Delete address by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Address/5c6f2bc5-8168-44ff-9ee9-18526325d923
        /// 
        /// Deletes address with the same ID, if exists
        /// </remarks>
        /// <param name="addressId">Address ID</param>
        /// <response code="204">Successful Operation</response>
        /// <response code="404">Address with the same ID not found</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpDelete("{addressId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid addressId)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var command = new DeleteAddressCommand()
                {
                    AddressId = addressId
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
