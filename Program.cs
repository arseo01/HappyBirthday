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
<<<<<<< HEAD

                          policy.WithOrigins("http://127.0.0.1:5500", "http://127.0.0.1:5501", "https://celebratebirthdays.github.io")
=======
                          policy.WithOrigins("http://127.0.0.1:5500", "http://127.0.0.1:5501", "https://celebratebirthdays.github.io", "https://arseo01.github.io")
>>>>>>> 712846a911964b88139b7c503fd82a832cef974f
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