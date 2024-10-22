namespace backend.Routers
{
  public static class PlaylistRouter
  {
    public static void RegisterRoutes(IEndpointRouteBuilder endpoints)
    {
      var playlistGroup = endpoints.MapGroup("/api/playlist");

      playlistGroup.MapControllerRoute(
        name: "Get Playlist",
        pattern: "/get",
        defaults: new { controller = "Playlist", action = "Get" });

      playlistGroup.MapControllerRoute(
        name: "Create Playlist",
        pattern: "/create",
        defaults: new { controller = "Playlist", action = "Create" });

      playlistGroup.MapControllerRoute(
        name: "Edit Playlist",
        pattern: "/edit",
        defaults: new { controller = "Playlist", action = "Edit" });

      playlistGroup.MapControllerRoute(
        name: "Delete Playlist",
        pattern: "/delete",
        defaults: new { controller = "Playlist", action = "Delete" });

      playlistGroup.MapControllerRoute(
        name: "Add Audio to Playlist",
        pattern: "/add-audio",
        defaults: new { controller = "Playlist", action = "AddAudio" });

      playlistGroup.MapControllerRoute(
        name: "Remove Audio from Playlist",
        pattern: "/remove-audio",
        defaults: new { controller = "Playlist", action = "RemoveAudio" });
    }
  }
}