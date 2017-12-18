using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkUploadTestDataGenerator
{
    public class DataRow
    {
        /// <summary>
        /// "Predicted" Id of object once persisted in db
        /// </summary>
        public long Id { get; set; }
        public string CohortRef { get; set; }
        public string ULN { get; set; }
        public string FamilyName { get; set; }
        public string GivenNames { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ProgType { get; set; }
        public string FworkCode { get; set; }
        public string PwayCode { get; set; }
        public string StdCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalPrice { get; set; }
        public string EPAOrgID { get; set; }
        public string ProviderRef { get; set; }


    }
}
