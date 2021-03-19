using Api.IRepository;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class WaterMeterController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<WaterMeterController> _logger;
        private readonly IMapper _mapper;

        public WaterMeterController(IUnitOfWork unitOfWork, ILogger<WaterMeterController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // GET ALL WaterMeters
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWaterMeters()
        {
            try
            {
                var watermeters = await _unitOfWork.WaterMeters.GetAll();
                var results = _mapper.Map<IList<WaterMeterDTO>>(watermeters); // mapping entity objects provided with measurements into dto objects
                return Ok(watermeters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetWaterMeters)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }

        // GET ONE WaterMeter by Id
        //Postman tests to be done
        //tests without token
        //with the wrong token
        //with expired token
        //with right token
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWaterMeter(int id)
        {
            try
            {
                var watermeter = await _unitOfWork.WaterMeters.Get(q => q.Id == id, new List<string> { "MeasuringPoints", "Measurements" });
                var result = _mapper.Map<WaterMeterDTO>(watermeter); // mapping entity objects provided with measurements into dto objects
                return Ok(watermeter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetWaterMeter)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
    }
}
