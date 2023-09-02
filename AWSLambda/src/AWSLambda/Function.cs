using System.Collections.Generic;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambda;

public class Function
{
    [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
    public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var name = request.QueryStringParameters is not null && 
                   request.QueryStringParameters.TryGetValue("name", out var value) ? value : "No Name";
        context.Logger.Log($"Got Name: {name}");
        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = $"User name passed: {name}",
            Headers = new Dictionary<string, string>(),
            IsBase64Encoded = false,
            MultiValueHeaders = new Dictionary<string, IList<string>>()
        };
    }
}