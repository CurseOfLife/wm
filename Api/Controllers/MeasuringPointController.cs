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
    public class MeasuringPointController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MeasuringPointController> _logger;
        private readonly IMapper _mapper;


        //var routes = await _unitOfWork.Routes.GetAll(q=> q.User.Email.Equals(username), orderBy: mt => mt.OrderBy(m => m.Id), new List<string> {"MeasuringPoints", "WaterMeters" }); ;
        public MeasuringPointController(IUnitOfWork unitOfWork, ILogger<MeasuringPointController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        #region GET
        // GET ALL MeasuringPoints
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeasuringPoints([FromQuery] RequestParams requestParams)
        {
            try
            {
                var measuringPoints = await _unitOfWork.MeasuringPoints.GetAllPagedList(requestParams);
                var results = _mapper.Map<IList<MeasuringPointDTO>>(measuringPoints); // mapping entity objects provided with measurements into dto objects
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetMeasuringPoints)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }


        //Get all measuring points for a specific user
        //add authorized user only.. mobile 

        /// <summary>
        /// Get all measuring points with water meters for a specific user
        /// </summary>
        [HttpGet("user/{username}")]
        [Authorize(Roles = "AndroidUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMeasuringPoints([FromRoute]string username)
        {
            try
            {
                if (!_unitOfWork.User.Exists(username))
                    return NotFound();

                //filter by user
                //filter by is active measuring point
                //filter by measurement ???

                var measuringPoints = await _unitOfWork.MeasuringPoints.GetAll(q => q.User.Email.Equals(username), mt => mt.OrderBy(m => m.Id), new List<string> {"WaterMeters" });
                var results = _mapper.Map<IList<MeasuringPointDTO>>(measuringPoints); // mapping entity objects provided with measurements into dto objects
                return Ok(new {measuringpoints = results });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetMeasuringPoints)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }


        // GET ONE MeasuringPoint by Id
        [HttpGet("{id:int}", Name = "GetMeasuringPoint")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeasuringPoint(int id)
        {
            try
            {
                var measuringpoint = await _unitOfWork.MeasuringPoints.Get(q => q.Id == id, new List<string> { "WaterMeters" });
                var result = _mapper.Map<MeasuringPointDTO>(measuringpoint); // mapping entity objects provided with measurements into dto objects
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetMeasuringPoint)}");
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
        public async Task<IActionResult> CreateMeasuringPoint([FromBody] CreateMeasuringPointDTO measuringPointDTO)
        {
            //checking if the received data is in the right format
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt inside {nameof(CreateMeasuringPoint)}");
                return BadRequest(ModelState);
            }

            try
            {
                var measuringPoint = _mapper.Map<MeasuringPoint>(measuringPointDTO);
                await _unitOfWork.MeasuringPoints.Insert(measuringPoint);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetMeasuringPoint", new { id = measuringPoint.Id }, measuringPoint);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(CreateMeasuringPoint)}");
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
        public async Task<IActionResult> UpdateMeasuringPoint(int id, [FromBody] UpdateMeasuringPointDTO measuringPointDTO)
        {
            if (id < 1 || !ModelState.IsValid)
            {
                _logger.LogError($"Invalid update attempt {nameof(UpdateMeasuringPoint)}");
                return BadRequest(ModelState);
            }

            try
            {
                var measuringPoint = await _unitOfWork.MeasuringPoints.Get(q => q.Id == id);

                if (measuringPoint == null)
                {
                    _logger.LogError($"Invalid update attempt {nameof(UpdateMeasuringPoint)}");
                    return BadRequest("Data is invalid");
                }

                //put measurementdto into measurement mapper
                _mapper.Map(measuringPointDTO, measuringPoint);
                _unitOfWork.MeasuringPoints.Update(measuringPoint);
                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(UpdateMeasuringPoint)}");
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
        public async Task<IActionResult> DeleteMeasuringPoint(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid delete attempt {nameof(DeleteMeasuringPoint)}");
                return BadRequest(ModelState);
            }

            try
            {
                var measuringPoint = await _unitOfWork.MeasuringPoints.Get(q => q.Id == id);
                if (measuringPoint == null)
                {
                    _logger.LogError($"Invalid delete attempt {nameof(DeleteMeasuringPoint)}");
                    return BadRequest("Data is invalid");
                }

                await _unitOfWork.MeasuringPoints.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(DeleteMeasuringPoint)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        #endregion
    }
}
