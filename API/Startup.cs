using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			services.AddDbContext<DataContext>(options =>
			{
				options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
			});
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", policy =>
				{
					policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
				});
			});
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