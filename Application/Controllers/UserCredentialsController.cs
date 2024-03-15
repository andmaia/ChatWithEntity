﻿using Application.Crosscuting.DTO.Credentials;
using Application.Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCredentialsController : ControllerBase
    {
        private readonly IUserCredentialsService _userCredentialsService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserCredentialsController(IUserCredentialsService userCredentialsService, SignInManager<IdentityUser> signInManager)
        {
            _userCredentialsService = userCredentialsService;
            _signInManager = signInManager;
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserCredentialsRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userCredentialsService.CreateUserAsync(model);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserCredentialsLogin data)
        {
            var result = await _userCredentialsService.LoginUserAsync(data);

            if (result.Success)
            {
                return Ok(new { Token = result.Data });
            }
            else
            {
                return BadRequest(new { Message = result.MessageError });
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Faz logout do usuário
            return Ok(); // Retorna um código de status 200 (OK)
        }


    }
}