namespace backend.DTO.AudioDTO
{
  namespace backend.DTO
  {
    public class DeleteAudioRequest
    {
      public int AudioId { get; set; } // The ID of the audio to delete
      public int UserId { get; set; } // Ensure the correct user is deleting their own audio
    }

    public class DeleteAudioResult
    {
      public int StatusCode { get; set; }
      public string? Message { get; set; }
    }
  }

}