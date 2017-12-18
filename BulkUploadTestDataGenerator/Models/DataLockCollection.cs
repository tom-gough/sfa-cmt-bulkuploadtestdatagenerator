using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkUploadTestDataGenerator.Models
{
    public class DataLockCollection
    {
        public int PageNumber { get; set; }
        public int TotalNumberOfPages { get; set; }
        public List<DataLockStatus> Items { get; set; }

        public DataLockCollection()
        {
            Items = new List<DataLockStatus>();
        }
    }
}
