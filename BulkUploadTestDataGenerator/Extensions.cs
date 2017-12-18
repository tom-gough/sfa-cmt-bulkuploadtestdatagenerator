using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BulkUploadTestDataGenerator
{
    public static class Extensions
    {
        public static T RandomElement<T>(this IList<T> q)
        {
            var r = new Random();
            return q[RandomHelper.GetRandomNumber(q.Count)];
        }
    }
}
