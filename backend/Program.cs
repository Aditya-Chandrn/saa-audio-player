using backend.Config;
using backend.Routers;
using backend.Services;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend;

public class Program
{
	public static void Main(string[] args)
	{
		DotNetEnv.Env.Load();

		var builder = WebApplication.CreateBuilder(args);
		// Add services to the container.
		builder.Services.AddControllersWithViews();
		builder.Services.ConfigDb();

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
		app.UseStaticFiles();
		app.UseRouting();
		app.UseAuthorization();
		app.Run();
	}
}
