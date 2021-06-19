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
    //github.com/swagger-api/swagger-core/wiki/annotations do some anotations research it

    [Route("api/[controller]")]
    [ApiController]
    public class ReadingStatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReadingStatusController> _logger;
        private readonly IMapper _mapper;

        public ReadingStatusController(IUnitOfWork unitOfWork, ILogger<ReadingStatusController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        #region GET
        /// <summary>
        /// Get all reading statuses
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "AndroidUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReadingStatuses()
        {
            try
            {
                var readingstatuses = await _unitOfWork.ReadingStatuses.GetAll();
                var results = _mapper.Map<IList<ReadingStatusDTO>>(readingstatuses); // mapping entity objects provided with measurements into dto objects
                return Ok(new { readingstatuses = results });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetReadingStatuses)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }

        // GET ONE Status by Id
        [HttpGet("{id:int}", Name = "GetReadingStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReadingStatus(int id)
        {
            try
            {
                var readingstatus = await _unitOfWork.ReadingStatuses.Get(q => q.Id == id, new List<string> { "Measurements" });
                var result = _mapper.Map<ReadingStatusDTO>(readingstatus); // mapping entity objects provided with measurements into dto objects
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetReadingStatus)}");
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
        public async Task<IActionResult> CreateReadingStatus([FromBody] CreateReadingStatusDTO readingStatusDTO)
        {
            //checking if the received data is in the right format
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt inside {nameof(CreateReadingStatus)}");
                return BadRequest(ModelState);
            }

            try
            {
                var readingStatus = _mapper.Map<ReadingStatus>(readingStatusDTO);
                await _unitOfWork.ReadingStatuses.Insert(readingStatus);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetReadingStatus", new { id = readingStatus.Id }, readingStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(CreateReadingStatus)}");
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
        public async Task<IActionResult> UpdateReadingStatus(int id, [FromBody] UpdateReadingStatusDTO readingStatusDTO)
        {
            if (id < 1 || !ModelState.IsValid)
            {
                _logger.LogError($"Invalid update attempt {nameof(UpdateReadingStatus)}");
                return BadRequest(ModelState);
            }

            try
            {
                var readingStatus = await _unitOfWork.ReadingStatuses.Get(q => q.Id == id);

                if (readingStatus == null)
                {
                    _logger.LogError($"Invalid update attempt {nameof(UpdateReadingStatus)}");
                    return BadRequest("Data is invalid");
                }

                //put measurementdto into measurement mapper
                _mapper.Map(readingStatusDTO, readingStatus);
                _unitOfWork.ReadingStatuses.Update(readingStatus);
                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(UpdateReadingStatus)}");
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
        public async Task<IActionResult> DeleteReadingStatus(int id)
        {
            if (id < 1)
            {

                _logger.LogError($"Invalid delete attempt {nameof(DeleteReadingStatus)}");
                return BadRequest(ModelState);
            }

            try
            {
                var readingStatus = await _unitOfWork.ReadingStatuses.Get(q => q.Id == id);
                if (readingStatus == null)
                {
                    _logger.LogError($"Invalid delete attempt {nameof(DeleteReadingStatus)}");
                    return BadRequest("Data is invalid");
                }

                await _unitOfWork.ReadingStatuses.Delete(id);
                await _unitOfWork.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(DeleteReadingStatus)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }

        }
        #endregion
    }
}
