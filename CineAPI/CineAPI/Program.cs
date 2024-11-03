using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using CineRepository.Repositories.Implementations;
using CineRepository.Services.Implementations;
using CineRepository.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

<<<<<<< HEAD


builder.Services.AddScoped<IUserAchievementsRepository, UserAchievementsRepository>();
builder.Services.AddScoped<IUserAchievementsService, UserAchievementsService>();
=======
builder.Services.AddDbContext<Cine_1W3_TPContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();
//builder.Services.AddScoped<IMoviesService, MoviesService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
>>>>>>> feddd10ab34a04a54fea0425ca0fbf6a85dfec70

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
