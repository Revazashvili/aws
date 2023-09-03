using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Mvc;

namespace DynamoDBAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly IDynamoDBContext _dynamoDbContext;

    public WeatherForecastController(IDynamoDBContext dynamoDbContext) => 
        _dynamoDbContext = dynamoDbContext;

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get(string city = "Brisbane")
    {
        return await _dynamoDbContext
            .QueryAsync<WeatherForecast>(city,QueryOperator.Between,new List<object>()
            {
                DateTime.UtcNow.Date,
                DateTime.UtcNow.Date.AddDays(2),
            })
            .GetRemainingAsync();
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(string city)
    {
        foreach (var weatherForecast in GenerateDummyWeatherForecasts(city))
            await _dynamoDbContext.SaveAsync(weatherForecast);
        
        return Ok();
    }
    
    [HttpPut]
    public async Task Put([FromQuery]string city,[FromQuery]DateTime date,
        [FromBody]string summary)
    {
        var weather = await _dynamoDbContext.LoadAsync<WeatherForecast>(city, date);
        weather.Summary = summary;
        await _dynamoDbContext.SaveAsync(weather);
    }

    [HttpDelete]
    public async Task Put([FromQuery] string city, [FromQuery] DateTime date) =>
        await _dynamoDbContext.DeleteAsync<WeatherForecast>(city, date);

    private IEnumerable<WeatherForecast> GenerateDummyWeatherForecasts(string city)
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                City = city,
                Date = DateTime.Now.Date.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}