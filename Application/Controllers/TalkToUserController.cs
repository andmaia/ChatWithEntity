using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Crosscuting.DTO.Talk;
using Application.Crosscuting.DTO.TalkToUser;
using Application.Domain.Entity;
using Application.Domain.Service;
using Application.Validators;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TalkToUserController : ControllerBase
    {
        private readonly ITalkToUserService _talkToUserService;

        public TalkToUserController(ITalkToUserService talkToUserService)
        {
            _talkToUserService = talkToUserService;
        }

        [HttpPost("archive/{id}")]
        [Authorize]

        public async Task<IActionResult> ArchiveTalk(string id)
        {
            var validator = new ParamsIdValidator();
            var validationResult = validator.Validate(id);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var result = await _talkToUserService.ArchiveTalk(id);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.MessageError);
            }
        }

        [HttpPost("create")]
        [Authorize]

        public async Task<IActionResult> CreateTalk([FromBody] TalkRequest data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _talkToUserService.CreateTalk(data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.MessageError);
            }
        }

        [HttpPost("favorite")]
        [Authorize]

        public async Task<IActionResult> FavoriteMessage(string messageId, string talkToUserId)
        {
            var validator = new ParamsIdValidator();
            var validationResult = validator.Validate(messageId);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var validationResultUser = validator.Validate(talkToUserId);

            if (!validationResultUser.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            var result = await _talkToUserService.FavoriteMessage(messageId, talkToUserId);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.MessageError);
            }
        }

        [HttpGet("talk/{talkId}")]
        [Authorize]

        public async Task<IActionResult> GetAllByTalk(string talkId)
        {
            var validator = new ParamsIdValidator();
            var validationResult = validator.Validate(talkId);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var result = await _talkToUserService.GetAllByTalk(talkId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.MessageError);
            }
        }

        [HttpGet("user/{userId}")]
        [Authorize]

        public async Task<IActionResult> GetAllByUser(string userId)
        {
            var validator = new ParamsIdValidator();
            var validationResult = validator.Validate(userId);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var result = await _talkToUserService.GetAllByUser(userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.MessageError);
            }
        }

        [HttpGet("talk/{talkId}/user/{userId}")]
        [Authorize]

        public async Task<IActionResult> GetAllByUserTalk(string talkId, string userId)
        {
            var validator = new ParamsIdValidator();
            var validationResult = validator.Validate(talkId);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var validationResultUser = validator.Validate(userId);

            if (!validationResultUser.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var result = await _talkToUserService.GetAllByUserTalk(talkId, userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.MessageError);
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
         
            var result = await _talkToUserService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.MessageError);
            }
        }
    }
}
