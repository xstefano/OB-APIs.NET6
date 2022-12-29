using APIVersionControl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// 1. Add HttpClient to send HttpRequests in controllers
builder.Services.AddHttpClient();

// 2. Add App Versioning Control
builder.Services.AddApiVersioning(setup =>
{
	setup.DefaultApiVersion = new ApiVersion(1, 0);
	setup.AssumeDefaultVersionWhenUnspecified = true;
	setup.ReportApiVersions = true;
});

// 3. Add configuration to document versions:
builder.Services.AddVersionedApiExplorer(setup =>
{
	setup.GroupNameFormat = "'v'VVV"; // 1. 0.0, 1.1.0 etc
	setup.SubstituteApiVersionInUrl = true;
});




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4. Configure options:
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

// 5. Configure Endpoints for swagger DOCS for each of the versions of our API:
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();

	// 6. Configure Swagger DOCS:
	app.UseSwaggerUI(options =>
	{
		foreach ( var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
		{
			options.SwaggerEndpoint(
				$"/swagger/{description.GroupName}/swagger.json",
				description.GroupName.ToUpperInvariant()
				);
		}
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
