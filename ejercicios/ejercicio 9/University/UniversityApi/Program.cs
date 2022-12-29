using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityApi;
using UniversityApi.Data;
using UniversityApi.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Config Serilog:
builder.Host.UseSerilog((hostBuilderCtx, loggerConf) =>
{
    loggerConf
        .WriteTo.Console()
        .WriteTo.Debug()
        .ReadFrom.Configuration(hostBuilderCtx.Configuration);
});


// TODO: Connection with SQL Server Express.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Services of JWT Autorization:
builder.Services.AddJwtTokenServices(builder.Configuration);


// Add services to the container.
builder.Services.AddControllers();

// Add Custom Services (folder Services)
builder.Services.AddScoped<IStudentsService, StudentsService>();

// TODO: Add the rest of services:

// Add Authorization:
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// TODO: Config Swagger to take care of Autorization of JWT:


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using Bearer Scheme"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
			new OpenApiSecurityScheme
		        {
			        Reference = new OpenApiReference
			        {
				        Type = ReferenceType.SecurityScheme,
				        Id = "Bearer"
			        }
		        },
		    new string[]{ }
		}
    });
});

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Tell app to use Serilog:
app.UseSerilogRequestLogging();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Tell app to user CORS
app.UseCors("CorsPolicy");

app.Run();
