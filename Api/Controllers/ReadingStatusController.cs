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

        // GET ALL Statuses
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReadingStatuses()
        {
            try
            {
                var readingstatuses = await _unitOfWork.ReadingStatuses.GetAll();
                var results = _mapper.Map<IList<ReadingStatusDTO>>(readingstatuses); // mapping entity objects provided with measurements into dto objects
                return Ok(readingstatuses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetReadingStatuses)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }

        // GET ONE Status by Id
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReadingStatus(int id)
        {
            try
            {
                var readingstatus = await _unitOfWork.ReadingStatuses.Get(q => q.Id == id, new List<string> { "Measurements" });
                var result = _mapper.Map<ReadingStatusDTO>(readingstatus); // mapping entity objects provided with measurements into dto objects
                return Ok(readingstatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetReadingStatus)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later");
            }
        }
    }
}
