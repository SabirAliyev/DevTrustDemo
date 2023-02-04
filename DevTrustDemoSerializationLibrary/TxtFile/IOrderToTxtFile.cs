using NorthwindData;
using System.Collections.Generic;

namespace DevTrustDemoSerializationLibrary.TxtFile
{
    /// <summary>
    /// Write default <see cref="Order"/> entity fields to TXT file format
    /// </summary>
    public interface IOrderToTxtFile
    {
        bool WriteToTxtFile(List<Order> objects, string fileName);
    }
}
