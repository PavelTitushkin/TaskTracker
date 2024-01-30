using Microsoft.EntityFrameworkCore;
using TaskTracker.Abstractions.Repositories;
using TaskTracker.Abstractions.Services;
using TaskTracker.DataAccess.Repositories;
using TaskTracker.Db;
using TaskTracker.Filters;
using TaskTracker.Logic.Services;

namespace TaskTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //Add DbContext
            builder.Services.AddDbContext<TaskTrackerContext>(
                optionsBuilder =>
                {
                    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("TaskTracker"),
                        sqlOptionsBuilder =>
                        {
                            sqlOptionsBuilder.EnableRetryOnFailure();
                        })
                    .UseSnakeCaseNamingConvention();
                });

            //AddAutomapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Add services to the container.
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ApiExceptionFilter));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
