using AzureBooks.Models;
using AzureBooks.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(options =>
{
    options.UseCosmos(
        builder.Configuration.GetValue<string>("Azure:CosmosDb:Uri"),
        builder.Configuration.GetValue<string>("Azure:CosmosDb:Key"),
        builder.Configuration.GetValue<string>("Azure:CosmosDb:DatabaseName"));
});
builder.Services.AddScoped<HelperService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/index.html");
        return;
    }
    await next();
});

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();
