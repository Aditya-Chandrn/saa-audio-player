using backend.Services.AudioServices;
using backend.DTO;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
  [Route("api/audio")]
  public class AudioController : ControllerBase
  {
    private readonly AudioServices _audioServices;

    public AudioController(AudioServices audioServices)
    {
      _audioServices = audioServices;
    }

    //////// CREATE AUDIO //////////
    [HttpPost("create")]
    public async Task<IActionResult> CreateAudio([FromBody] AudioRequest createRequest)
    {
      if (createRequest == null)
        return BadRequest(new { Message = "Audio details not found" });

      // Create audio
      var result = await _audioServices.CreateAudio(createRequest.UserId, createRequest.AudioBase64String, createRequest.Title, createRequest.Album);
      return StatusCode(result.StatusCode, new { result.Message, result.Audio });
    }

    //////// GET AUDIO //////////
    [HttpGet("get")]
    public async Task<IActionResult> GetAudio([FromQuery] AudioRequest getRequest)
    {
      // Get audio details by ID
      var result = await _audioServices.GetAudio(getRequest.AudioId);
      if (result.Audio == null)
        return NotFound(new { Message = "Audio not found" });

      return Ok(new { result.Audio, result.Message });
    }

    //////// EDIT AUDIO //////////
    [HttpPatch("edit")]
    public async Task<IActionResult> EditAudio([FromBody] AudioRequest editRequest)
    {
      if (editRequest == null)
        return BadRequest(new { Message = "Audio details not found" });

      // Edit audio
      var result = await _audioServices.EditAudio(editRequest.AudioId, editRequest.Title, editRequest.Album);
      return StatusCode(result.StatusCode, new { result.Message, result.Audio });
    }

    //////// DELETE AUDIO //////////
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteAudio([FromBody] AudioRequest deleteRequest)
    {
      if (deleteRequest == null)
        return BadRequest(new { Message = "Audio details not found" });

      // Delete audio
      var result = await _audioServices.DeleteAudio(deleteRequest.AudioId);
      return StatusCode(result.StatusCode, new { result.Message });
    }
  }
}
