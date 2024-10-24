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
		Console.WriteLine(EnvVariables.CLIENT_URL);
		builder.Services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			});

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
		app.Run();
	}
}
