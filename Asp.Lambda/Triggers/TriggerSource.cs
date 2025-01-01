using Amazon.Lambda.APIGatewayEvents;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Staticsoft.Asp.Lambda;

public interface TriggerSource
{
    bool TryConvert(
        JsonElement request,
        JsonSerializerOptions options,
        [NotNullWhen(true)] out APIGatewayProxyRequest? proxyRequest
    );
}