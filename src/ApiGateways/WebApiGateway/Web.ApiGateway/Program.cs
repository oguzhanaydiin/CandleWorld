using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Web.ApiGateway.Infrastructure;
using Web.ApiGateway.Services;
using Web.ApiGateway.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.AddJsonFile("configurations/ocelot.json");

builder.Services.AddOcelot().AddConsul();

builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IBasketService, BasketService>();


// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    builder.SetIsOriginAllowed((host) => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
});

//creating http client access point
builder.Services.AddSingleton<IHttpContextAccessor>();

builder.Services.AddHttpClient("basket", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["urls:basket"]);
})
    .AddHttpMessageHandler<HttpClientDelegatingHandler>();

builder.Services.AddHttpClient("catalog", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["urls:catalog"]);
})
    .AddHttpMessageHandler<HttpClientDelegatingHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.UseOcelot().Wait();

app.MapControllers();


app.Run();
