using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Domain.Service;
using Application.Service;
using Application.Validators;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TalkController : ControllerBase
    {
        private readonly ITalkService _talkService;

        public TalkController(ITalkService talkService)
        {
            _talkService = talkService;
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetTalkById(string id)
        {
            try
            {
                var validator = new ParamsIdValidator();
                var validationResult = validator.Validate(id);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var result = await _talkService.GetTalkById(id);
                if (result.Success)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return NotFound(result.MessageError);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
            }

        }

        [HttpGet("user/{id}")]
        [Authorize]

        public async Task<IActionResult> GetTalksByUserId(string id)
        {
            try
            {
                var validator = new ParamsIdValidator();
                var validationResult = validator.Validate(id);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }


                var result = await _talkService.GetTallByUserId(id);
                if (result.Success)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return NotFound(result.MessageError);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
            }
        }
    }
}
