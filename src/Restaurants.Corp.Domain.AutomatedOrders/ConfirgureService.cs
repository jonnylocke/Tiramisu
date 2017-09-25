using Topshelf;

namespace Restaurants.Corp.Domain.StockOrders
{
    public static class ConfigureService
    {
        public static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<StockOrderService>(service =>
                {
                    service.ConstructUsing(s => new StockOrderService());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });

                configure.RunAsLocalSystem();
                configure.SetServiceName("StockOrderService");
                configure.SetDisplayName("Stock Order Service");
                configure.SetDescription("Service to order stock");
            });
        }
    }
}
