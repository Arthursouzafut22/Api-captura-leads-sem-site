using LeadSemSite.Application.Interfaces;
using LeadSemSite.Application.SerperService;
using LeadSemSite.Infrastructure;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ISerperService, SerperService>();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, ct) =>
    {
        document.Info = new OpenApiInfo
        {
            Title = "LeadSemSite API",
            Version = "v1",
            Description = "API para encontrar empresas sem site no Google Maps. " +
                          "Informe um termo de busca e uma cidade para listar leads, " +
                          "com opção de filtrar apenas empresas sem site e paginar os resultados.",
            Contact = new OpenApiContact
            {
                Name = "Arthur Souza",
                Email = "arthursouz.dev@gmail.com"
            }
        };

        return Task.CompletedTask;
    });
});

var app = builder.Build();

var swaggerHabilitado = app.Environment.IsDevelopment()
    || builder.Configuration.GetValue<bool>("Swagger:Enabled");

if (swaggerHabilitado)
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "LeadSemSite API v1");
        options.RoutePrefix = "swagger";
    });
}

if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
