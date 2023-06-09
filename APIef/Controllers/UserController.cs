﻿using APIef.Data;
using APIef.Interface;
using APIef.Models;
using APIef.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIef.Controllers
{
    [Authorize(Roles = "Regular, Admin")]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUsers _userService;


        public UserController(IUsers userService)
        {
            _userService = userService;
        }

        // GET api/users
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // GET api/users/{userName}
        [HttpGet("{userName}")]
        public async Task<IActionResult> GetUser(string userName)
        {
            try
            {
                var user = await _userService.GetUserAsync(userName);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // POST api/users
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCredentials userCredentials)
        {
            try
            {
                
                if (_userService.UserExists(userCredentials.UserName) == true)
                {
                    return BadRequest("User with this name already exists");
                }
                else
                {
                    await _userService.AddUserAsync(new User { UserName = userCredentials.UserName, Password = userCredentials.Password });
                    return CreatedAtAction(nameof(GetUser), new { userName = userCredentials.UserName }, userCredentials);
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // PUT api/users/{userName}
        [HttpPut("{userName}")]
        public async Task<IActionResult> UpdateUser(string userName, [FromBody] User user)
        {
            try
            {
                if (userName != user.UserName)
                {
                    return BadRequest();
                }

                await _userService.UpdateUserAsync(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // DELETE api/users/{userName}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            try
            {
                
                    await _userService.DeleteUserAsync(userName);
                    return Ok();
                
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
        [HttpPatch("{userName}")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword changePassword)
        {
            try
            {
                string res = await _userService.CheckCredentialsAsync(new UserCredentials { UserName = changePassword.UserName, Password = changePassword.OldPassword });
                if (res == "OK")
                {
                    await _userService.ChangePasswordAsync(new UserCredentials { UserName = changePassword.UserName, Password = changePassword.NewPassword });
                    return NoContent();
                }
                else
                {
                    return Unauthorized(res);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] UserCredentials userCredentials)
        {
            try
            {
                await _userService.AddUserAsync(new User { UserName = userCredentials.UserName, Password = userCredentials.Password, Role = new Role { RoleId = 3, Name = "Admin" } }) ;
                return CreatedAtAction(nameof(GetUser), new { userName = userCredentials.UserName }, userCredentials);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
        
    }
}
