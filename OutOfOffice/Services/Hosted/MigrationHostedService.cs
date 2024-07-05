using Microsoft.EntityFrameworkCore;
using OutOfOffice.Data;

namespace OutOfOffice.Services.Hosted
{
    public class MigrationHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public MigrationHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<OutOfOfficeContext>();
            
            context.Database.Migrate();
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
