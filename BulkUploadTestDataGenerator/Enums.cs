using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkUploadTestDataGenerator
{
    public enum ProblemType
    {
        StartDateOverlap = 0,
        EndDateOverlap = 1,
        DateWithin = 2,
        DateStraddle = 3
    }
}
