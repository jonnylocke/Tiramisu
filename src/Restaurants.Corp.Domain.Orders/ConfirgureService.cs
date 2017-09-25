using Topshelf;

namespace Restaurants.Corp.Domain.Orders
{
    internal static class ConfigureService
    {
        internal static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<OrdersService>(service =>
                {
                    service.ConstructUsing(s => new OrdersService());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });
                //Setup Account that window service use to run.  
                configure.RunAsLocalSystem();
                configure.SetServiceName("OrdersService");
                configure.SetDisplayName("OrdersService");
                configure.SetDescription("To Process Restaurant Orders");
            });
        }
    }
}
