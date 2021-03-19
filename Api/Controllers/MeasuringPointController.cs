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
    public class MeasuringPointController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MeasuringPointController> _logger;
        private readonly IMapper _mapper;

        public MeasuringPointController(IUnitOfWork unitOfWork, ILogger<MeasuringPointController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // GET ALL MeasuringPoints
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeasuringPoints()
        {
            try
            {
                var measuringPoints = await _unitOfWork.MeasuringPoints.GetAll();
                var results = _mapper.Map<IList<MeasuringPointDTO>>(measuringPoints); // mapping entity objects provided with measurements into dto objects
                return Ok(measuringPoints);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetMeasuringPoints)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }

        // GET ONE MeasuringPoint by Id
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeasuringPoint(int id)
        {
            try
            {
                var measuringpoint = await _unitOfWork.MeasuringPoints.Get(q => q.Id == id, new List<string> { "Route", "WaterMeters" });
                var result = _mapper.Map<MeasuringPointDTO>(measuringpoint); // mapping entity objects provided with measurements into dto objects
                return Ok(measuringpoint);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetMeasuringPoint)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
    }
}
