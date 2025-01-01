using Amazon.Lambda.APIGatewayEvents;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Staticsoft.Asp.Lambda;

public class RequestTrigger : TriggerSource
{
    public bool TryConvert(
        JsonElement request,
        JsonSerializerOptions options,
        [NotNullWhen(true)] out APIGatewayProxyRequest? proxyRequest
    )
    {
        proxyRequest = null;
        if (!request.TryGetProperty("HttpMethod", out var _)) return false;

        proxyRequest = JsonSerializer.Deserialize<APIGatewayProxyRequest>(request, options)!;
        return true;
    }
}