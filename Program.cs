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
        // Add all the origins where the frontend is hosted
        policy.WithOrigins(
            "http://127.0.0.1:5500",  // Local development
            "https://arseo01.github.io",  // GitHub pages frontend
            "http://127.0.0.1:5501",  // Local dev for other environment
            "https://celebratebirthday.github.io"  // GitHub pages frontend
        )
        .AllowAnyHeader()  // Allow all headers
        .AllowAnyMethod();  // Allow all HTTP methods (GET, POST, etc.)
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Commented out HTTPS redirection for Render compatibility
// app.UseHttpsRedirection();

app.UseCors("localhostDevelopment");  // Apply the CORS policy

app.UseAuthorization();

app.MapControllers();

app.Run();