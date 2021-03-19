using Api.Models;
using Api.Services;
using AutoMapper;
using Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManagerService _authManager;

        public AccountController(UserManager<ApiUser> userManager,         
            ILogger<AccountController> logger,
            IMapper mapper,
            IAuthManagerService authManager)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }

        //Registration endpoint
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO) 
        {
            _logger.LogInformation($"Registration Attempted by {userDTO.Email}");

            //tried to register but didnt include the right data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            try
            {
                //creating the user..hashing password etc
                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email;
                var result = await _userManager.CreateAsync(user, userDTO.Password);

                //something went wrong
                if (!result.Succeeded)
                {
                    //returning error data can contain sensitive data so be careful
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    
                    return BadRequest(ModelState);
                }

                await _userManager.AddToRolesAsync(user, userDTO.Roles);
                return Accepted();

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, $"Something went wrong while trying {nameof(Register)}");
                return Problem($"Something went wrong while trying {nameof(Register)}", statusCode:500);
            }  
        }


        
        //Registration endpoint
        //test token with jwt.io
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            _logger.LogInformation($"Login Attempted by {userDTO.Email}");

            //tried to register but didnt include the right data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _authManager.ValidateUser(userDTO)) 
                {
                    return Unauthorized();
                }

                //if login information is correct we return a 202 code with the token
                return Accepted(new { Token = await _authManager.CreateToken()});         
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, $"Something went wrong while trying {nameof(Login)}");
                return Problem($"Something went wrong while trying {nameof(Login)}", statusCode: 500);
            }
        }
    }
}
