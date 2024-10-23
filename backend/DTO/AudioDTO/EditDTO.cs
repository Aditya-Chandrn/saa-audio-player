namespace backend.DTO.AudioDTO
{
  namespace backend.DTO
  {
    public class EditAudioRequest
    {
      public int AudioId { get; set; } // The ID of the audio to edit
      public int UserId { get; set; } // The ID of the user editing the audio
      public string? Title { get; set; } // Optional: Edit audio title
      public string? AudioBase64String { get; set; } // Optional: Edit the audio file
      public string? Album { get; set; } // Optional: Edit album name
    }

    public class EditAudioResult
    {
      public int StatusCode { get; set; }
      public string? Message { get; set; }
    }
  }

}