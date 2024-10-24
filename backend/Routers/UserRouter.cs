namespace backend.Routers
{
  public static class UserRouter
  {
    public static void RegisterRoutes(IEndpointRouteBuilder endpoints)
    {
      var userGroup = endpoints.MapGroup("api/user");

      userGroup.MapControllerRoute(
        name: "Login",
        pattern: "login",
        defaults: new { controller = "User", action = "Login" });

      userGroup.MapControllerRoute(
        name: "Signup",
        pattern: "signup",
        defaults: new { controller = "User", action = "Signup" });

      userGroup.MapControllerRoute(
        name: "Edit User",
        pattern: "edit",
        defaults: new { controller = "User", action = "EditUser" });

      userGroup.MapControllerRoute(
        name: "Delete User",
        pattern: "delete",
        defaults: new { controller = "User", action = "DeleteUser" });

      userGroup.MapControllerRoute(
        name: "Get User Playlists",
        pattern: "get-playlists",
        defaults: new { controller = "User", action = "GetUserPlaylists" });
    }
  }
}