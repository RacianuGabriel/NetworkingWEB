using API.Extension;
using Application.Activities;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;


namespace API
{
	public class Startup
	{
		public IConfiguration _config { get; }
		public Startup(IConfiguration config)
		{
			_config = config;

		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddApplicationServices(_config);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			//app.UseHttpsRedirection();
			app.UseRouting();

			app.UseCors("CorsPolicy");

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}