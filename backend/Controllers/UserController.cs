using backend.Config;
using backend.DTO.UserDTO;
using backend.Services.UserServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
  [Route("/api/user")]
  public class UserController : ControllerBase
  {
    private readonly UserServices _userServices;
    public UserController(UserServices userServices)
    {
      _userServices = userServices;
    }

    //////// LOGIN //////////
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
      if (loginRequest == null)
        return BadRequest(new { Message = "Login credentials not found" });

      // check if no field empty
      if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
      {
        return BadRequest(new { Message = "Username or password can't be empty" });
      }

      var result = await _userServices.Login(loginRequest.Username, loginRequest.Password);
      return StatusCode(result.StatusCode, new { result.Message, result.User });
    }

    //////// SIGNUP //////////
    [HttpPost("/signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest signupRequest)
    {
      if (signupRequest == null)
        return BadRequest(new { Message = "User credentials not found" });

      // check if no field empty
      if (string.IsNullOrEmpty(signupRequest.Username) || string.IsNullOrEmpty(signupRequest.Email) || string.IsNullOrEmpty(signupRequest.Password))
      {
        return BadRequest(new { Message = "Username, password or email can't be empty" });
      }

      // create user
      var result = await _userServices.Signup(signupRequest.Username, signupRequest.Email, signupRequest.Password);
      return StatusCode(result.StatusCode, new { result.Message });
    }

    //////// SIGNUP //////////
    [HttpPatch("/edit")]
    public async Task<IActionResult> EditUser([FromBody] EditUserRequest editUserRequest)
    {
      if (editUserRequest == null)
        return BadRequest(new { Message = "User credentials not found" });

      // edit user
      var result = await _userServices.EditUser(editUserRequest.UserId, editUserRequest.Username, editUserRequest.Email, editUserRequest.Password, editUserRequest.Image);
      return StatusCode(result.StatusCode, new { result.Message, result.User });
    }

    //////// SIGNUP //////////
    [HttpDelete("/delete")]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest deleteUserRequest)
    {
      if (deleteUserRequest == null)
        return BadRequest(new { Message = "User credentials not found" });

      // delete user
      var result = await _userServices.DeleteUser(deleteUserRequest.UserId);
      return StatusCode(result.StatusCode, new { result.Message });
    }
  }
}