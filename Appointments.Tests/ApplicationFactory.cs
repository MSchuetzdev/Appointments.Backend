using System.Net;
using System.Net.Http.Headers;
using Appointments.Api;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Appointments.Tests;

public class ApplicationFactory : IDisposable
{
    private IServiceScope? _scope;

    public WebApplicationFactory<Program> Factory { get; private set; }

    public ApplicationFactory()
    {
        // Instruct dapper to map camel_case field from database to PascalCase: first_name => FirstName to improve database results in test-environment 
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureLogging(options =>
                {
                    options.ClearProviders();
                    options.AddConsole();
                });
                builder.ConfigureServices(services =>
                {
                    services.AddTransient<FakeRemoteIpAddressMiddleware>();
                    services.AddSingleton<IStartupFilter, StartupFilter>();
                });
            });
    }

    public T GetScopedService<T>() where T : notnull
    {
        _scope ??= Factory.Services.CreateScope();
        return _scope.ServiceProvider.GetRequiredService<T>();
    }

    public ApplicationFactory WithProductionEnvironment()
    {
        Factory = Factory.WithWebHostBuilder(builder => { builder.UseEnvironment("Production"); });

        return this;
    }

    public ApplicationFactory WithDevelopmentEnvironment()
    {
        Factory = Factory.WithWebHostBuilder(builder => { builder.UseEnvironment("Development"); });

        return this;
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        return GetScopedService<IMediator>().Send(request: request, cancellationToken: cancellationToken);
    }

    /*public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : notnull
    {
        return GetScopedService<IMediator>().Send(request: request, cancellationToken: cancellationToken);
    }*/


    public HttpClient CreateClientWithAuth()
    {
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Guid.NewGuid().ToString());
        return client;
    }

    public void Dispose()
    {
        _scope?.Dispose();
        Factory.Dispose();
    }

    public class FakeRemoteIpAddressMiddleware : IMiddleware
    {
        private readonly IPAddress _fakeIpAddress = IPAddress.Parse("127.0.0.1");

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Connection.RemoteIpAddress = _fakeIpAddress;
            return next(context);
        }
    }

    public class StartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
            => (app) =>
            {
                app.UseMiddleware<FakeRemoteIpAddressMiddleware>();
                next(app);
            };
    }
}