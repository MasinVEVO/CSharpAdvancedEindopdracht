using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapGet("/fetchdata", async () =>
{
    using (HttpClient client = new HttpClient())
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync("https://climate.weensum.nl/");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return Results.Ok(responseBody);
        }
        catch (HttpRequestException e)
        {
            return Results.Problem($"Request error: {e.Message}");
        }
    }
});

app.MapPost("/senddata", async (HttpRequest request) =>
{
    using (HttpClient client = new HttpClient())
    {
        try
        {
            string requestBody;
            using (var reader = new StreamReader(request.Body))
            {
                requestBody = await reader.ReadToEndAsync();
            }

            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://climate.weensum.nl/", content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return Results.Ok(responseBody);
        }
        catch (HttpRequestException e)
        {
            return Results.Problem($"Request error: {e.Message}");
        }
    }
});

app.Run();