using NorthwindData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTrustDemo.Services
{
    public interface ICsvRowConvertable<T> where T : CsvSerializable
    {
        bool WriteToCsvFile(List<T> objects, string fileName);
        
    }
}
