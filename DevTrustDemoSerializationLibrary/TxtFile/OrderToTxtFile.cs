using NorthwindData;
using System;
using System.Collections.Generic;

namespace DevTrustDemoSerializationLibrary.TxtFile
{
    public class OrderToTxtFile : IOrderToTxtFile
    {
        public bool WriteToTxtFile(List<Order> objects, string fileName)
        {
            /// Due to the fact that there are no specific instructions on how exactly to write data in text format, 
            /// the implementation as for CSV format used.
            throw new NotImplementedException();
        }
    }
}
