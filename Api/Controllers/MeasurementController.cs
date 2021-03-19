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
    //atrribute routing (this) vs convention routing
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MeasurementController> _logger;
        private readonly IMapper _mapper;
      

        public MeasurementController(IUnitOfWork unitOfWork, 
            ILogger<MeasurementController> logger, 
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;        
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeasurements()
        {
            try
            {
                var measurements = await _unitOfWork.Measurements.GetAll();
                var results = _mapper.Map<IList<MeasurementDTO>>(measurements); // mapping entity objects provided with measurements into dto objects
                return Ok(measurements);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetMeasurements)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeasurement(int id)
        {
            try
            {
                var measurement = await _unitOfWork.Measurements.Get(q => q.Id ==id, new List<string> {"WaterMeter", "ReadingStatus"});
                var result = _mapper.Map<MeasurementDTO>(measurement); // mapping entity objects provided with measurements into dto objects
                return Ok(measurement);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetMeasurement)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
    }
}
