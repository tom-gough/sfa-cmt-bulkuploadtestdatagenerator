using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkUploadTestDataGenerator
{
    public static class RandomHelper
    {
        private static readonly Random Rnd = new Random();

        public static int GetRandomNumber(int limit)
        {
            return Rnd.Next(limit);
        }
    }
}
