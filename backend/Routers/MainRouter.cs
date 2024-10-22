namespace backend.Routers
{
  public static class MainRouter
  {
    public static void RegisterRoutes(IEndpointRouteBuilder endpoints)
    {
      // User
      UserRouter.RegisterRoutes(endpoints);

      // Audio
      AudioRouter.RegisterRoutes(endpoints);

      // Playlist
      PlaylistRouter.RegisterRoutes(endpoints);
    }
  }
}