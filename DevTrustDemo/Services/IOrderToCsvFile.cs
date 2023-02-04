using NorthwindData;
using System.Collections.Generic;

namespace DevTrustDemo.Services
{
    public interface IOrderToCsvFile
    {
        bool WriteToCsvFile(List<Order> objects, string fileName);
        
    }
}
