using Azure.Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Interfaces;
using ProjectAPI.Repository;

namespace ProjectAPI {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            {
                // Add services to the container.
                builder.Services.AddControllers();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
                builder.Services.AddScoped<ITeamRepository, TeamRepository>();
                builder.Services.AddScoped<IClientRepository, ClientRepository>();
                builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
                builder.Services.AddScoped<ITaskRepository, TaskRepository>();
                builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                //Add data context to the container
                builder.Services.AddDbContext<DataContext>(options => {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                });
            }

            var app = builder.Build();
            {

                if (app.Environment.IsDevelopment()) {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseExceptionHandler(options => {
                    options.Run(async context => {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        context.Response.ContentType = ContentType.ApplicationJson.ToString();

                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (contextFeature != null) {
                            await context.Response.WriteAsJsonAsync(new {
                                StatusCode = context.Response.StatusCode,
                                Message = contextFeature.Error.Message
                            });
                        }
                    });
                });

                // Configure the HTTP request pipeline.
                app.UseHttpsRedirection();
                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
        }
    }
}