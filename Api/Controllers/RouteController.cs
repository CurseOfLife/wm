using Api.IRepository;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RouteController> _logger;
        private readonly IMapper _mapper;

        public RouteController(IUnitOfWork unitOfWork, ILogger<RouteController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // GET ALL Routes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoutes()
        {
            try
            {
                var routes = await _unitOfWork.Routes.GetAll();
                var results = _mapper.Map<IList<RouteDTO>>(routes); // mapping entity objects provided with measurements into dto objects
                return Ok(routes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetRoutes)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }

        // GET ONE Route by Id
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoute(int id)
        {
            try
            {
                var route = await _unitOfWork.MeasuringPoints.Get(q => q.Id == id, new List<string> { "MeasuringPoints" });
                var result = _mapper.Map<RouteDTO>(route); // mapping entity objects provided with measurements into dto objects
                return Ok(route);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetRoute)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
    }
}
