using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkUploadTestDataGenerator.Models
{
    public class DataLockStatus
    {
        public DataLockStatus()
        {
            Errors = new List<DataLockError>();
            Periods = new List<DataLockPeriod>();
            Apprenticeships = new List<DataLockApprenticeship>();
        }

        public long Id { get; set; }
        public DateTime ProcessDateTime { get; set; }
        public string IlrFileName { get; set; }
        public long Ukprn { get; set; }
        public long Uln { get; set; }
        public string Status { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public string PriceEpisodeIdentifier { get; set; }
        public long ApprenticeshipId { get; set; }
        public long EmployerAccountId { get; set; }
        public string EventSource { get; set; }
        public bool HasErrors
        {
            get
            {
                return Errors.Any();
            }
        }
        public DateTime IlrStartDate { get; set; }
        public int IlrProgrammeType { get; set; }
        public int? IlrFrameworkCode { get; set; }
        public int? IlrPathwayCode { get; set; }
        public int? IlrStandardCode { get; set; }
        public int IlrTrainingPrice { get; set; }
        public int IlrEndpointAssessorPrice { get; set; }
        public DateTime IlrPriceEffectiveDate { get; set; }
        public DateTime IlrPriceEffectiveFromDate { get; set; }
        public List<DataLockError> Errors { get; set; }
        public List<DataLockPeriod> Periods { get; set; }
        public List<DataLockApprenticeship> Apprenticeships { get; set; }
    }

    public class DataLockError
    {
        public string ErrorCode { get; set; }
        public string SystemDescription { get; set; }
    }
    public class DataLockPeriod { }
    public class DataLockApprenticeship { }

}
