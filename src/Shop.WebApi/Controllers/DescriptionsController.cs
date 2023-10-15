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

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DescriptionVm>))]
        public async Task<ActionResult<IEnumerable<DescriptionVm>>> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var query = new GetAllDescriptionsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{descriptionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(DescriptionVm))]
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

        [HttpPost("{shoesId}")]
        [ProducesResponseType(409)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Guid))]
        public async Task<IActionResult> CreateDescription(Guid shoesId, [FromBody] DescriptionDto descriptionDto)
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
        }

        [HttpDelete("{descriptionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
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

        [HttpPut("{descriptionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateDescription(Guid descriptionId, [FromBody] DescriptionDto descriptionDto)
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