using backend.Config;
using backend.Routers;
using backend.Utils;

namespace backend;

public class Program
{
	public static void Main(string[] args)
	{
		DotNetEnv.Env.Load();

		var builder = WebApplication.CreateBuilder(args);

		// add services
		builder.Services.AddControllersWithViews();
		builder.Services.ConfigDb();
		builder.Services.AddCors(options =>
<<<<<<< HEAD
						{
							options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
						});
=======
			{
				options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			});
>>>>>>> 749e031314a3f01a71e37bd57b78d48993d14611

		builder.WebHost.UseUrls($"{EnvVariables.HOST}:{EnvVariables.PORT}");

		var app = builder.Build();
		MainRouter.RegisterRoutes(app);

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseRouting();
		app.UseCors("CorsPolicy");
		app.UseAuthorization();
		app.MapControllers();
		app.Run();
	}
}
