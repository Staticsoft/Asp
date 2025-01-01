using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Amazon.Lambda.Serialization.SystemTextJson.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Staticsoft.Asp.Lambda;

public abstract class LambdaEntrypoint<Startup>
    where Startup : class
{
    readonly ApiGatewayEntryPoint<Startup> Entrypoint = new();
    readonly static JsonSerializerOptions Options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = new AwsNamingPolicy(),
        Converters =
        {
            new DateTimeConverter(),
            new MemoryStreamConverter(),
            new ConstantClassConverter(),
            new ByteArrayConverter()
        }
    };

    protected virtual TriggerCollection Triggers
        => new([]);

    public Task<APIGatewayProxyResponse> FunctionHandlerAsync(JsonElement request, ILambdaContext lambdaContext)
    {
        foreach (var source in Triggers)
        {
            if (source.TryConvert(request, Options, out var proxyRequest))
            {
                return Entrypoint.FunctionHandlerAsync(proxyRequest, lambdaContext);
            }
        }

        var serializedRequest = JsonSerializer.Serialize(request, Options);
        throw new NotSupportedException($"Unable to handle request: {serializedRequest}");
    }
}
