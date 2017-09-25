using Restaurant.Corp.Adapters.Dtos;
using System.IO;
using Newtonsoft.Json;

namespace RestaurantX.Web.Adapters
{
    public class ViewPresenter : IViewPresenter
    {
        public StockReportModel GetIncomingStockReport()
        {
            RestaurantStock stock = null;

            var path = "c:\\temp\\stockshipped.json";

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
                ReportTitle = "Incoming Stock Being Delivered This Month",
                Stock = stock
            };

            return reportModel;
        }
    }
}
