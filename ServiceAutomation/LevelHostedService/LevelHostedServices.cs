using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevelHostedService
{
    public class LevelHostedServices : BackgroundService, IDisposable
    {
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            //Some logic for making levels

            await Task.CompletedTask;
        }
    }
}
