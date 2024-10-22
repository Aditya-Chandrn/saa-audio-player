using backend.Services;
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
		}
	}
}