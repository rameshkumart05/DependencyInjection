using Dependency_Injection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

// Register the services
using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<TransientService>();
        services.AddScoped<ScopedService>();
        services.AddSingleton<SingletonService>();
    })
    .Build();

// Resolve services from the root container
Console.WriteLine("=== Root scope ===");

var transient1 = host.Services
    .GetRequiredService<TransientService>();
var transient2 = host.Services
    .GetRequiredService<TransientService>();

var singleton1 = host.Services
    .GetRequiredService<SingletonService>();
var singleton2 = host.Services
    .GetRequiredService<SingletonService>();

Console.WriteLine($"Transient 1: {transient1.Id}");
Console.WriteLine($"Transient 2: {transient2.Id}");
Console.WriteLine($"Singleton 1: {singleton1.Id}");
Console.WriteLine($"Singleton 2: {singleton2.Id}");

// Create scope 1
using (var scope1 = host.Services.CreateScope())
{
    Console.WriteLine("\n=== Scope 1 ===");

    var scoped1 = scope1.ServiceProvider
        .GetRequiredService<ScopedService>();
    var scoped2 = scope1.ServiceProvider
        .GetRequiredService<ScopedService>();

    Console.WriteLine($"Scoped 1: {scoped1.Id}");
    Console.WriteLine($"Scoped 2: {scoped2.Id}");
}

// Create scope 2
using (var scope2 = host.Services.CreateScope())
{
    Console.WriteLine("\n=== Scope 2 ===");

    var scoped3 = scope2.ServiceProvider
        .GetRequiredService<ScopedService>();

    Console.WriteLine($"Scoped 3: {scoped3.Id}");

    var singleton3 = scope2.ServiceProvider
        .GetRequiredService<SingletonService>();

    Console.WriteLine($"Singleton 3: {singleton3.Id}");
}


namespace Dependency_Injection
{
    internal class DI_Lifetime
    {
    }
public class TransientService
{
    public Guid Id { get; } = Guid.NewGuid();
}

public class ScopedService
{
    public Guid Id { get; } = Guid.NewGuid();
}

public class SingletonService
{
    public Guid Id { get; } = Guid.NewGuid();
}
}


