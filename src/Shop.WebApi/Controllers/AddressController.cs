using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        ///     POST /api/Address/
        ///     {
        ///       "country": "Russia",
        ///       "city": "Moscow",
        ///       "street": "Falling street",
        ///       "house": "14",
        ///       "room": 2
        ///     }
        ///
        /// Return: ID of created address
        /// </remarks>
        /// <param name="addressDto">Address</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([Required] AddressDto addressDto)
        {
            var command = Mapper.Map<CreateAddressCommand>(addressDto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get all addresses
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Address
        ///     
        /// Return: List of all addresses
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AddressVm>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AddressVm>>> GetAll()
        {
            var query = new GetAllAddressesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get address by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Address/e034b7ef-03c0-4c01-aa00-55a39cbcfcd7
        /// 
        /// Return: Single address with the given Id, if exists
        /// </remarks>
        /// <param name="addressId">Address Id</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="404">Address with the same Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("{addressId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddressVm>> GetById([Required] Guid addressId)
        {
            var query = new GetAddressQuery()
            {
                AddressId = addressId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Update address information
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Address/5da752ea-3a18-4bd9-aaca-66519c23a049
        ///     {
        ///       "country": "Russia",
        ///       "city": "Moscow",
        ///       "street": "Falling street",
        ///       "house": "14",
        ///       "room": 2
        ///     }
        /// 
        /// </remarks>
        /// <param name="addressId">Address Id</param>
        /// <param name="descriptionDto">New address</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="404">Address with given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPut("{addressId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([Required] Guid addressId, [Required] AddressDto descriptionDto)
        {
            var command = Mapper.Map<UpdateAddressCommand>(descriptionDto);
            command.AddressId = addressId;
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Delete address
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Address/5c6f2bc5-8168-44ff-9ee9-18526325d923
        /// 
        /// </remarks>
        /// <param name="addressId">Address Id</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="404">Address with the given Id not found</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpDelete("{addressId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] Guid addressId)
        {
            var command = new DeleteAddressCommand()
            {
                AddressId = addressId
            };
            await Mediator.Send(command);
            return Ok();
        }
    }
}
