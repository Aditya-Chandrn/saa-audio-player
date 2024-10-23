using backend.Services.AudioServices;
using backend.Services.PlaylistServices;
using backend.Services.UserServices;
using backend.Utils;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace backend.Config
{
	public static class Database
	{

		public static void ConfigDb(this IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
			options.UseNpgsql(EnvVariables.SUPABASE_URI));

			services.AddScoped<UserServices>();
			services.AddScoped<AudioServices>();
			services.AddScoped<PlaylistServices>();
		}
	}
}