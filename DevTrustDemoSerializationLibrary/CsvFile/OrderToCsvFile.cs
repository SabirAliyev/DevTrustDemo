using CsvHelper;
using CsvHelper.Configuration;
using NorthwindData;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace DevTrustDemoSerializationLibrary.CsvFile
{
    public class OrderToCsvFile : IOrderToCsvFile
    {
        public int WriteToCsvFile(List<Order> objects, string fileName)
        {
            int iterator = 0;
            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Encoding = Encoding.UTF8,
                Delimiter = ","
            };

            using (var outputFile = new StreamWriter(fileName)) {

                using (CsvWriter writer = new CsvWriter(outputFile, config)) {
                    
                    writer.WriteField("orderId");
                    writer.WriteField("customerId");
                    writer.WriteField("emploeeId");
                    writer.WriteField("orderDate");
                    writer.WriteField("requireDate");
                    writer.WriteField("shippedDate");
                    writer.WriteField("shipVia");
                    writer.WriteField("freight");
                    writer.WriteField("shipName");
                    writer.WriteField("shipAddress");
                    writer.WriteField("shipCity");
                    writer.WriteField("shipRegion");
                    writer.WriteField("shipPostalCode");
                    writer.WriteField("shipCountry");

                    writer.NextRecord();

                    foreach (var obj in objects) {
                        writer.WriteField(obj.OrderID);
                        writer.WriteField(obj.CustomerID);
                        writer.WriteField(obj.EmployeeID);
                        writer.WriteField(obj.OrderDate);
                        writer.WriteField(obj.RequiredDate);
                        writer.WriteField(obj.ShipVia);
                        writer.WriteField(obj.Freight);
                        writer.WriteField(obj.ShipName);
                        writer.WriteField(obj.ShipAddress);
                        writer.WriteField(obj.ShipCity);
                        writer.WriteField(obj.ShipRegion);
                        writer.WriteField(obj.ShipPostalCode);
                        writer.WriteField(obj.ShipCountry);

                        iterator++;
                        writer.NextRecord();
                    }
                }
            }
            return iterator;
        }
    }
}
