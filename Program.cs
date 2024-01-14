using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Sakila.App.WebAPI.Context;
using Sakila.App.WebAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SakilaContext>();
builder.Services.AddTransient<IFilmService, FilmService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IRentalService, RentalService>();

builder.Services.AddControllers()
    .AddJsonOptions(jsonOptions => {
        jsonOptions.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "Sakila API",
        Description = "An API using the Sakila demo database created by MySQL. The database contains data of a fictional DVD rental store.",
        Contact = new OpenApiContact
        {
            Name = "Joan Almonte",
            Url = new Uri("https://almontej.net")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
