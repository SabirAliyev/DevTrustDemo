using NorthwindData;
using System.Collections.Generic;

namespace DevTrustDemoSerializationLibrary.CsvFile
{
    /// <summary>
    /// Write default <see cref="Order"/> entity fields to CSV file format
    /// </summary>
    public interface IOrderToCsvFile
    {
        int WriteToCsvFile(List<Order> objects, string fileName);
    }
}
