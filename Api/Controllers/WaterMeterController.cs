using Api.IRepository;
using Api.Models;
using Api.Models.Create;
using Api.Models.Update;
using AutoMapper;
using Domain;
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

        #region GET
        // GET ALL WaterMeters
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWaterMeters([FromQuery] RequestParams requestParams)
        {
            try
            {
                var watermeters = await _unitOfWork.WaterMeters.GetAllPagedList(requestParams);
                var results = _mapper.Map<IList<WaterMeterDTO>>(watermeters); // mapping entity objects provided with measurements into dto objects
                return Ok(results);
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
        [HttpGet("{id:int}", Name ="GetWaterMeter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWaterMeter(int id)
        {
            try
            {
                var watermeter = await _unitOfWork.WaterMeters.Get(q => q.Id == id, new List<string> { "MeasuringPoints", "Measurements" });
                var result = _mapper.Map<WaterMeterDTO>(watermeter); // mapping entity objects provided with measurements into dto objects
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetWaterMeter)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
        #endregion

        #region POST
        //add [Authorize(Roles= "Administrator", "")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateWaterMeter([FromBody] CreateWaterMeterDTO waterMeterDTO)
        {
            //checking if the received data is in the right format
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt inside {nameof(CreateWaterMeter)}");
                return BadRequest(ModelState);
            }

            try
            {
                var watermeter = _mapper.Map<WaterMeter>(waterMeterDTO);
                await _unitOfWork.WaterMeters.Insert(watermeter);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetWaterMeter", new { id = watermeter.Id }, watermeter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(CreateWaterMeter)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }

        }
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
        public async Task<IActionResult> UpdateWaterMeter(int id, [FromBody] UpdateWaterMeterDTO waterMeterDTO)
        {
            if (id < 1 || !ModelState.IsValid)
            {
                _logger.LogError($"Invalid update attempt {nameof(UpdateWaterMeter)}");
                return BadRequest(ModelState);
            }

            try
            {
                var waterMeter = await _unitOfWork.WaterMeters.Get(q => q.Id == id);

                if (waterMeter == null)
                {
                    _logger.LogError($"Invalid update attempt {nameof(UpdateWaterMeter)}");
                    return BadRequest("Data is invalid");
                }

                //put measurementdto into measurement mapper
                _mapper.Map(waterMeterDTO, waterMeter);
                _unitOfWork.WaterMeters.Update(waterMeter);
                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(UpdateWaterMeter)}");
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
        public async Task<IActionResult> DeleteWaterMeter(int id)
        {
            if (id < 1)
            {

                _logger.LogError($"Invalid delete attempt {nameof(DeleteWaterMeter)}");
                return BadRequest(ModelState);
            }

            try
            {
                var waterMeter = await _unitOfWork.WaterMeters.Get(q => q.Id == id);
                if (waterMeter == null)
                {
                    _logger.LogError($"Invalid delete attempt {nameof(DeleteWaterMeter)}");
                    return BadRequest("Data is invalid");
                }

                await _unitOfWork.WaterMeters.Delete(id);
                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(DeleteWaterMeter)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }

        }
        #endregion

    }
}
