using Amazon.DynamoDBv2.DataModel;

namespace DynamoDBAPI;

public class WeatherForecast
{
    [DynamoDBHashKey]
    public string City { get; set; }
    
    [DynamoDBRangeKey]
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string? Summary { get; set; }
}