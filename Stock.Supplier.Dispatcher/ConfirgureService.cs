using Topshelf;

namespace Stock.Supplier.Dispatcher
{
    internal static class ConfigureService
    {
        internal static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<WarehouseDispatcher>(service =>
                {
                    service.ConstructUsing(s => new WarehouseDispatcher());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });
                
                configure.RunAsLocalSystem();
                configure.SetServiceName("WarehouseDispatcher");
                configure.SetDisplayName("Stock Supplier Dispatcher Service");
                configure.SetDescription("Service ship food suppliers");
            });
        }
    }
}
