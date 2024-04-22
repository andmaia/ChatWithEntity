using Application.Crosscuting.DTO.Credentials;
using Application.Crosscuting.DTO.User;
using Application.Crosscuting.Helpers;
using Application.Domain.Entity;
using Application.Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Adicione este namespace
using Application.Service;
using Application.Validators;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Application.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("{userId}/photo")]
        [Authorize]

        public async Task<IActionResult> UpdatePhotoUser(string userId, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Nenhum arquivo enviado.");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var data = memoryStream.ToArray();

                    var validationResult = FileValidator.ValidateFile(data);
                    if (!validationResult.Success)
                    {
                        return BadRequest(validationResult.MessageError);
                    }

                    var result = await _userService.UpdatePhotoUser(data, userId);
                    if (result.Success)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(result.MessageError);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao processar a solicitação: {ex.Message}");
            }
        }


        [HttpPost("create")]
        [Authorize]

        public async Task<IActionResult> CreateUser([FromBody] UserRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await  _userService.CreateUser(model);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetById(string id)
        {
            var validator = new ParamsIdValidator();
            var validationResult = validator.Validate(id);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _userService.GetUserById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.MessageError);
            }
        }


        [HttpGet("{id}/credentials")]
        [Authorize]
        public async Task<IActionResult> GetByIdCredentials(string id)
        {
            var validator = new ParamsIdValidator();
            var validationResult = validator.Validate(id);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _userService.GetUserByCredentialsId(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.MessageError);
            }
        }

        [HttpGet]
        [Authorize]

        public async Task<IActionResult> GetAll()
        {
            var users =await _userService.GetAllUsers();
            if (users.Success)
            {
                return Ok(users.Data);
            }
            else
            {
                return BadRequest();
            };
        }

    }
}
