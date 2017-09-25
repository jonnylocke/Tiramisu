using Restaurant.Corp.Adapters.Dtos;
using System.IO;
using Newtonsoft.Json;

namespace RestaurantCorp.Adapters
{
    public class ViewPresenter : IViewPresenter
    {
        public StockReportModel GetStockItemToOrder()
        {
            RestaurantStock stock = null;

            var path = "c:\\temp\\stockorder.json";
            if (File.Exists(path))
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    var items = JsonConvert.DeserializeObject<RestaurantStock>(json);
                    stock = items;
                }
            }
            
            var reportModel = new StockReportModel
            {
                ReportTitle = "Items of Stock that Require Ordering",
                Stock = stock
            };

            return reportModel;
        }
    }
}
