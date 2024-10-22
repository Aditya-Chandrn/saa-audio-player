namespace backend.Routers
{
  public static class AudioRouter
  {
    public static void RegisterRoutes(IEndpointRouteBuilder endpoints)
    {
      var audioGroup = endpoints.MapGroup("/api/audio");

      audioGroup.MapControllerRoute(
        name: "Get Audio",
        pattern: "/get",
        defaults: new { controller = "Audio", action = "Get" });

      audioGroup.MapControllerRoute(
        name: "Create Audio",
        pattern: "/create",
        defaults: new { controller = "Audio", action = "Create" });

      audioGroup.MapControllerRoute(
        name: "Edit Audio",
        pattern: "/edit",
        defaults: new { controller = "Audio", action = "Edit" });
        
      audioGroup.MapControllerRoute(
        name: "Delete Audio",
        pattern: "/delete",
        defaults: new { controller = "Audio", action = "Delete" });
    }
  }
}