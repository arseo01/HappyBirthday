using backend.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("localhostDevelopment", policy =>
    {
        policy.WithOrigins(
            "http://127.0.0.1:5501", // Local dev environment
            "https://celebratebirthday.github.io" // GitHub Pages frontend
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Commented out HTTPS redirection for Render compatibility
// app.UseHttpsRedirection();

app.UseCors("localhostDevelopment"); // Apply the CORS policy

app.UseAuthorization();

app.MapControllers();

app.Run();