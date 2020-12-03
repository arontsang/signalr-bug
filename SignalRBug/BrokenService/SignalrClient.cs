using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using SignalRBug.Hub;

namespace SignalRBug.BrokenService
{
    public class SignalrClient
        : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(100, stoppingToken);

            var client = new HubConnectionBuilder()
                .WithUrl("http://localhost:3001/signalr")
                .Build();
            await client.StartAsync();

            await client
                .StreamAsyncCore<Bar>(nameof(FooHub.StreamBar), new object[]{})
                // This next line breaks everything.
                .ToObservable()
                .Do(Console.WriteLine)
                .ToTask(stoppingToken);
        }
    }
}