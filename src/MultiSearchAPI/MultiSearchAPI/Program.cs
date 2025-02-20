using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MultiSearch API",
        Version = "v1",
        Description = "API para busca de múltiplas fontes de dados - Desenvolvedor Júnior SEED.",
        Contact = new OpenApiContact
        {
            Name = "Francisco Marcello Ribeiro Lima",
            Email = "franciscomarcello17@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/franciscomarcello/")
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MultiSearch API v1");
        options.RoutePrefix = "swagger";
    });
}
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
