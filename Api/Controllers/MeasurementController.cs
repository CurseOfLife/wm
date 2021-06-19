using Api.IRepository;
using Api.Models;
using Api.Models.Update;
using AutoMapper;
using Domain;
using Marvin.Cache.Headers;
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


        #region GET
        //caching server side, on every controller we can set how long .. in this case 60s
        //postman settings headers send no-cache header  OFF
        //not needed as we are using a library check service extensions ConfigureHTTPCacheHeader
        //[ResponseCache(CacheProfileName ="60SecondsDuration")]
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 125)]
        [HttpCacheValidation(MustRevalidate = false)] //if data changes cache wont hold latest version
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

        [HttpGet("{id:int}", Name = "GetMeasurement")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeasurement(int id)
        {
            try
            {
                var measurement = await _unitOfWork.Measurements.Get(q => q.Id == id, new List<string> { "WaterMeter", "ReadingStatus" });
                var result = _mapper.Map<MeasurementDTO>(measurement); // mapping entity objects provided with measurements into dto objects
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetMeasurement)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }

        /*
        //array keys: 1,2,3...10..
        [HttpGet("({ids})", Name = "GetMeasurementCollection")]
        public IActionResult GetMeasurementCollection([FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        { //implicit binding needed to get the different ids from route which is done with custom model binders
          //passed as param
            if (ids == null)
                return BadRequest();


            var measurements = _unitOfWork.Measurements.GetBands(ids); //have all bands that match ids from url

            //we have to check if the ids we got are the same as from database
            //we do that by doing Count on bandEntities
            if (ids.Count() != bandEntities.Count())
                return NotFound();

            var bandsToReturn = _mapper.Map<IEnumerable<BandDto>>(bandEntities);

            return Ok(bandsToReturn);
        }*/

        #endregion

        #region POST
        //add [Authorize(Roles= "Administrator", "")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMeasurement([FromBody] CreateMeasurementDTO measurementDTO)
        {
            //checking if the received data is in the right format
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt inside {nameof(CreateMeasurement)}");
                return BadRequest(ModelState);
            }

            try
            {
                var measurement = _mapper.Map<Measurement>(measurementDTO);
                await _unitOfWork.Measurements.Insert(measurement);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetMeasurement", new { id = measurement.Id }, measurement);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(CreateMeasurement)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }


        /*
        //add [Authorize(Roles= "Administrator", "")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<MeasurementDTO>>> CreateMeasurements([FromBody] IEnumerable<CreateMeasurementDTO> measurementCollection)
        {
            //checking if the received data is in the right format
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt inside {nameof(CreateMeasurement)}");
                return BadRequest(ModelState);
            }


            try
            {
                var measurements = _mapper.Map<IEnumerable<Measurement>>(measurementCollection);

                foreach (var m in measurements)
                {
                    await _unitOfWork.Measurements.Insert(m);
                }
                           
                await _unitOfWork.Save();

                //objects to return
                var measurementsToReturn = _mapper.Map<IEnumerable<MeasurementDTO>>(measurements);
                var idsString = string.Join(",", measurementsToReturn.Select(a => a.Id));

                return CreatedAtRoute("GetMeasurementCollection", new { ids = idsString }, measurementsToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(CreateMeasurement)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }

        }*/
        #endregion

        #region PUT
        //add [Authorize]
        //diff ways of doing (int id, [FromBody]CreateMeasurementDTO measurementDTO) dto has the id
        //other one same, but dto doesnt need to have the id
        //in this one we use the 2nd example where the dto doesnt have id
        //put replaces data puts null to missing fields ..client updated 3 out of 4.. 4th is set to null
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateMeasurement(int id, [FromBody] UpdateMeasurementDTO measurementDTO)
        {
            if (id < 1 || !ModelState.IsValid)
            {
                _logger.LogError($"Invalid update attempt {nameof(UpdateMeasurement)}");
                return BadRequest(ModelState);
            }

            try
            {
                var measurement = await _unitOfWork.Measurements.Get(q => q.Id == id);

                if (measurement == null)
                {
                    _logger.LogError($"Invalid update attempt {nameof(UpdateMeasurement)}");
                    return BadRequest("Data is invalid");
                }

                //put measurementdto into measurement mapper
                _mapper.Map(measurementDTO, measurement);
                _unitOfWork.Measurements.Update(measurement);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(UpdateMeasurement)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
        #endregion

        #region DELETE
        //[Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMeasurement(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid delete attempt {nameof(DeleteMeasurement)}");
                return BadRequest(ModelState);
            }

            try
            {
                var measurement = await _unitOfWork.Measurements.Get(q => q.Id == id);
                if (measurement == null)
                {
                    _logger.LogError($"Invalid delete attempt {nameof(DeleteMeasurement)}");
                    return BadRequest("Data is invalid");
                }

                await _unitOfWork.Measurements.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(DeleteMeasurement)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        #endregion

    }
}
