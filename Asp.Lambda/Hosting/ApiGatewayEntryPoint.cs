using Amazon.Lambda.AspNetCoreServer;
using Microsoft.AspNetCore.Hosting;

namespace Staticsoft.Asp.Lambda;

class ApiGatewayEntryPoint<Startup> : APIGatewayProxyFunction
    where Startup : class
{
    protected override void Init(IWebHostBuilder builder)
        => builder.UseStartup<Startup>();
}
