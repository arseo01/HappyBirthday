var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "localhostDevelopment",
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5500", "https://arseo01.github.io", "http://127.0.0.1:5501", "https://celebratebirthday.github.io")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("localhostDevelopment");

app.UseAuthorization();

app.MapControllers();

app.Run();
