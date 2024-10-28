using backend.Config;
using backend.DTO;
using backend.Models;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.PlaylistServices
{
  public partial class PlaylistServices
  {
    public async Task<PlaylistResponse> GetPlaylist(int? playlistId)
    {
      try
      {
        var existingPlaylist = await _context.Playlists.Where(p => p.Id == playlistId)
                                                      .Select(p => new
                                                      {
                                                        p.Id,
                                                        p.Name,
                                                        p.ImageBase64String,
                                                        Audios = p.PlaylistAudios.Select(pa => new
                                                        {
                                                          pa.Audio.Id,
                                                          pa.Audio.Title,
                                                          pa.Audio.Album
                                                        }).ToList()
                                                      })
                                                      .FirstOrDefaultAsync();

        if (existingPlaylist == null)
        {
          new PrintFailure($"Playlist '{playlistId}' not found");
          return new PlaylistResponse { StatusCode = 404, Message = "Playlist not found" };
        }

        var playlistData = new PlaylistData
        {
          PlaylistId = existingPlaylist.Id,
          Name = existingPlaylist.Name,
          ImageBase64String = existingPlaylist.ImageBase64String,
          Audios = existingPlaylist.Audios.Select(audio => new AudioInfo
          {
            AudioId = audio.Id,
            Title = audio.Title,
            Album = audio.Album
          }).ToList()
        };


        new PrintSuccess($"Playlist '{playlistId}' fetched");
        return new PlaylistResponse { Playlist = playlistData, StatusCode = 200, Message = "Playlist fetched successfully" };
      }
      catch (Exception exception)
      {
        new PrintError($"Error fetching playlist '{playlistId}'", exception);
        return new PlaylistResponse { StatusCode = 500, Message = "Error fetching playlist" };
      }
    }
  }
}
