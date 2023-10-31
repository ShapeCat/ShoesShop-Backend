//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using ShoesShop.Application.Exceptions;
//using ShoesShop.Application.Requests.Commands;
//using ShoesShop.Application.Requests.Queries;
//using ShoesShop.Application.Requests.Queries.OutputVMs;
//using ShoesShop.WebApi.Dto;

//namespace ShoesShop.WebAPI.Controllers
//{
//    public class ShoesController : AbstractController
//    {
//        public ShoesController(IMapper mapper) : base(mapper) { }

//        /// <summary>
//        /// Return list of all shoes
//        /// </summary>
//        /// <remarks>
//        /// Sample request:
//        /// 
//        ///     GET /api/Shoes
//        ///     
//        /// Returns list of all shoes
//        /// </remarks>
//        /// <returns>List of all shoes</returns>
//        /// <response code="200">Successful Operation</response>
//        /// <response code="400">Invalid request</response>
//        /// <response code="500">Server Error. Please, report administrator</response>
//        [HttpGet]
//        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ShoesVm>))]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<ActionResult<IEnumerable<ShoesVm>>> GetAll()
//        {
//            if (!ModelState.IsValid) return BadRequest(ModelState);

//            var query = new GetAllShoesQuery();
//            var result = await Mediator.Send(query);
//            return Ok(result);
//        }

//        /// <summary>
//        /// Find shoes by ID
//        /// </summary>
//        /// <remarks>
//        /// Sample request:
//        /// 
//        ///     GET /api/Shoes/a34d2083-1bec-4d68-b8bb-ce179222131b
//        /// 
//        /// Returns single shoes with the same ID, if exists
//        /// </remarks>
//        /// <returns>Shoes with the same ID, if exists</returns>
//        /// <param name="shoesId">Shoes ID</param>
//        /// <response code="200">Successful Operation</response>
//        /// <response code="400">Invalid request</response>
//        /// <response code="404">Shoes with the same ID not found</response>
//        /// <response code="500">Server Error. Please, report administrator</response>
//        [HttpGet("{shoesId}")]
//        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoesVm))]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<ActionResult<ShoesVm>> GetShoes(Guid shoesId)
//        {
//            if (!ModelState.IsValid) return BadRequest(ModelState);
//            try
//            {
//                var query = new GetShoesQuery()
//                {
//                    ShoesId = shoesId
//                };
//                var result = await Mediator.Send(query);
//                return Ok(result);
//            }
//            catch (NotFoundException ex)
//            {
//                return NotFound(ex.Message);
//            }
//        }

//        /// <summary>
//        /// Find all shoes sizes
//        /// </summary>
//        /// <remarks>
//        /// Sample request:
//        /// 
//        ///     GET /api/Shoes/33e30f06-b7bd-4b81-872f-225b88583700/sizes
//        /// 
//        /// Returns list of all sizes of shoes with the same ID
//        /// </remarks>
//        /// <returns>List of all sizes of shoes with the same ID</returns>
//        /// <param name="shoesId">Shoes ID</param>
//        /// <response code="200">Successful Operation</response>
//        /// <response code="400">Invalid request</response>
//        /// <response code="404">Shoes with the same ID not found</response>
//        /// <response code="500">Server Error. Please, report administrator</response>
//        [HttpGet("{shoesId}/sizes")]
//        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ShoesSizeVm>))]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<ActionResult<IEnumerable<ShoesSizeVm>>> GetShoesSizes(Guid shoesId)
//        {
//            if (!ModelState.IsValid) return BadRequest(ModelState);
//            try
//            {
//                var query = new GetShoesSizesByShoesQuery()
//                {
//                    ShoesId = shoesId
//                };
//                var result = await Mediator.Send(query);
//                return Ok(result);
//            }
//            catch (NotFoundException ex)
//            {
//                return NotFound(ex.Message);
//            }
//        }

//        /// <summary>
//        /// Find shoes description
//        /// </summary>
//        /// <remarks>
//        /// Sample request:
//        /// 
//        ///     GET /api/Shoes/f258ef1c-163d-4f88-b6b9-584e4d147343/description
//        /// 
//        /// Returns description of shoes with the same ID, if exists
//        /// </remarks>
//        /// <param name="shoesId">Shoes ID</param>
//        /// <response code="200">Successful Operation</response>
//        /// <response code="400">Invalid request</response>
//        /// <response code="404">Description for this shoes not found</response>
//        /// <response code="500">Server Error. Please, report administrator</response>
//        [HttpGet("{shoesId}/description")]
//        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DescriptionVm))]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<ActionResult<DescriptionVm>> GetShoesDescription(Guid shoesId)
//        {
//            if (!ModelState.IsValid) return BadRequest(ModelState);
//            try
//            {
//                var query = new GetDescriptionByShoesQuery()
//                {
//                    ShoesId = shoesId
//                };
//                var result = await Mediator.Send(query);
//                return Ok(result);
//            }
//            catch (NotFoundException ex)
//            {
//                return NotFound(ex.Message);
//            }
//        }

//        /// <summary>
//        /// Create shoes
//        /// </summary>
//        /// <remarks>
//        /// Sample request:
//        /// 
//        ///     POST /api/Shoes/
//        ///     {
//        ///         "Name": "Some shoes without name"
//        ///     }
//        /// 
//        /// Creates shoes 
//        /// Returns ID of created shoes
//        /// </remarks>
//        /// <param name="shoesDto">Shoes creation information</param>
//        /// <response code="200">Successful Operation</response>
//        /// <response code="400">Invalid request</response>
//        /// <response code="500">Server Error. Please, report administrator</response>
//        [HttpPost]
//        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<ActionResult> CreateShoes([FromBody] ShoesDto shoesDto)
//        {
//            if (!ModelState.IsValid) return BadRequest(ModelState);
//            if (shoesDto is null) return BadRequest(ModelState);
//            try
//            {
//                var command = mapper.Map<CreateShoesCommand>(shoesDto);
//                var result = await Mediator.Send(command);
//                return Ok(result);
//            }
//            catch (AlreadyExistsException ex)
//            {
//                return Conflict(ex.Message);
//            }
//        }

//        /// <summary>
//        /// Delete shoes by ID
//        /// </summary>
//        /// <remarks>
//        /// Sample request:
//        /// 
//        ///     DELETE /api/Shoes/449e04fa-4317-44fd-beae-4aa9df2456eb
//        /// 
//        /// Deletes shoes with the same ID, if exists
//        /// </remarks>
//        /// <param name="shoesId">Shoes ID</param>
//        /// <response code="204">Successful Operation</response>
//        /// <response code="404">Shoes with the same ID not found</response>
//        /// <response code="400">Invalid request</response>
//        /// <response code="500">Server Error. Please, report administrator</response>
//        [HttpDelete("{shoesId}")]
//        [ProducesResponseType(StatusCodes.Status204NoContent)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<ActionResult> DeleteShoes(Guid shoesId)
//        {
//            if (!ModelState.IsValid) return BadRequest();
//            try
//            {
//                var command = new DeleteShoesCommand()
//                {
//                    ShoesId = shoesId
//                };
//                await Mediator.Send(command);
//                return NoContent();
//            }
//            catch (NotFoundException ex)
//            {
//                return NotFound(ex.Message);
//            }
//        }

//        /// <summary>
//        /// Update shoes information by ID
//        /// </summary>
//        /// <remarks>
//        /// Sample request:
//        /// 
//        ///     PUT /api/Shoes/1a06943c-b9d1-40c3-80a0-107dd16d837f
//        ///     {
//        ///         "Name": "Some shoes without name"
//        ///     }
//        /// 
//        /// Updates shoes with the same ID, if exists
//        /// </remarks>
//        /// <param name="shoesId">Shoes ID</param>
//        /// <param name="shoesDto">New shoes information</param>
//        /// <response code="204">Successful Operation</response>
//        /// <response code="400">Invalid request</response>
//        /// <response code="404">Shoes with the same ID not found</response>
//        /// <response code="500">Server Error. Please, report administrator</response>
//        [HttpPut("{shoesId}")]
//        [ProducesResponseType(StatusCodes.Status204NoContent)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<ActionResult> UpdateShoes(Guid shoesId, [FromBody] ShoesDto shoesDto)
//        {
//            if (!ModelState.IsValid) return BadRequest(ModelState);
//            if (shoesDto is null) return BadRequest(ModelState);
//            try
//            {
//                var command = mapper.Map<UpdateShoesCommand>(shoesDto);
//                command.ShoesId = shoesId;
//                await Mediator.Send(command);
//                return NoContent();
//            }
//            catch (NotFoundException ex)
//            {
//                return NotFound(ex.Message);
//            }
//        }
//    }
//}