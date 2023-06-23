using System.Text;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
EnableForwardedHeaders(app);
app.MapGet("/", (ctx) => {

    var ipv4 = string.Empty;
    if (ctx.Request.Headers.ContainsKey("X-Forwarded-For"))
    {
        ipv4 = ctx.Request.Headers["X-Forwarded-For"].First();
    }
    else
    {
        var remoteIpAddress = ctx.Connection.RemoteIpAddress;
        if(remoteIpAddress != null)
        {
            ipv4 = remoteIpAddress.ToString();
        }
    }
    _ = ctx.Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(ipv4));
    return Task.CompletedTask;
});
app.MapGet("/hello", () => {

    return "Hello World!";

});



app.Run();


static void EnableForwardedHeaders(IApplicationBuilder app)
{
    
    var forwardedHeadersOptions = new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
        RequireHeaderSymmetry = false
    };
    forwardedHeadersOptions.KnownNetworks.Clear();
    forwardedHeadersOptions.KnownProxies.Clear();
    app.UseForwardedHeaders(forwardedHeadersOptions);

}