using Microsoft.EntityFrameworkCore;
using Backend_website.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

// Register controllers
builder.Services.AddControllers();

// Configure CORS policy for React app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000") // Allow your React app's URL
                     .AllowAnyHeader()
                     .AllowAnyMethod();
    });
});

// Configure Swagger/OpenAPI for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply CORS policy globally
app.UseCors("AllowReactApp");

// Register the custom exception middleware
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
