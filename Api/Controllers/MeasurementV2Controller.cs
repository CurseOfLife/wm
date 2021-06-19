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
    //test by
    //api/measurement?api-version=2.0

    //or [Route("api/{v:apiversion}/measurement")]
    //api/2.0/measurement

    //or opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
    //api/measurement
    //client adds in header api-version .. value is 2.0

    //testing how to version api 
    //deprecated is for old versions.. it will show it to the user in the header as api-deprecated-version not api-version
    [ApiVersion("2.0", Deprecated = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementV2Controller : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MeasurementController> _logger;
        private readonly IMapper _mapper;

        public MeasurementV2Controller(IUnitOfWork unitOfWork,
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
        public async Task<IActionResult> GetMeasurements([FromQuery] RequestParams requestParams)
        {
            try
            {
                var measurements = await _unitOfWork.Measurements.GetAllPagedList(requestParams);
                var results = _mapper.Map<IList<MeasurementDTO>>(measurements); // mapping entity objects provided with measurements into dto objects
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetMeasurements)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
    }
}
