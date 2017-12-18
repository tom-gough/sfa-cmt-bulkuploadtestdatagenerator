using BulkUploadTestDataGenerator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BulkUploadTestDataGenerator
{
    class Program
    {
        internal const string OutputPath = @"c:\temp\";

        static void Main(string[] args)
        {
            var recordCount = ConsoleHelper.ReadInteger("Number of records to generate: ");
            Console.WriteLine("Enter cohort: ");
            var cohort = Console.ReadLine().Trim();

            var data = new List<DataRow>();

            for (var i = 0; i < recordCount; i++)
            {
                var startdate = DataHelper.GetRandomStartDate();
                var course = DataHelper.GetRandomTrainingCourse();

                var row = new DataRow
                {
                    Id = DataHelper.GetNextApprenticeshipId(),
                    CohortRef = cohort,
                    ULN = DataHelper.GetRandomUniqueULN(),
                    GivenNames = DataHelper.GetRandomFirstName(),
                    FamilyName = DataHelper.GetRandomLastName(),
                    DateOfBirth = DataHelper.GetRandomDateOfBirth(),
                    StartDate = startdate,
                    EndDate = DataHelper.GetRandomEndDate(startdate),
                    StdCode = course.StandardCode,
                    PwayCode = course.PwayCode,
                    FworkCode = course.FworkCode,
                    ProgType = course.ProgType,
                    TotalPrice = 2000
                };

                data.Add(row);
            }

            //Create folder
            Directory.CreateDirectory($"{OutputPath}{cohort}");

            //Create bulk upload file
            OutputFile(data, $"{OutputPath}{cohort}\\bulkupload.csv");

            //Duplicates
            var duplicates = CreateDuplicateDataset(data);
            OutputFile(duplicates, $"{OutputPath}{cohort}\\_duplicates.csv");

            //Datalocks
            var datalocks = CreateDataLockCollection(data);
            var datalockJson = JsonConvert.SerializeObject(datalocks);
            File.WriteAllText($"{OutputPath}{cohort}\\1_payment_event.json", datalockJson);

            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        private static List<DataRow> CreateDuplicateDataset(List<DataRow> source)
        {
            var duplicateCount = source.Count / 2;

            var duplicates = source.OrderBy(x => x.ULN).Take(duplicateCount).ToList();

            foreach (var duplicate in duplicates)
            {
                var problem = DataHelper.GetRandomProblemType();

                switch (problem)
                {
                    case ProblemType.StartDateOverlap:
                        duplicate.StartDate = duplicate.StartDate.AddMonths(-3);
                        duplicate.EndDate = duplicate.EndDate.AddMonths(-3);
                        break;
                    case ProblemType.EndDateOverlap:
                        duplicate.StartDate = duplicate.StartDate.AddMonths(3);
                        duplicate.EndDate = duplicate.EndDate.AddMonths(3);
                        break;
                    case ProblemType.DateWithin:
                        duplicate.StartDate = duplicate.StartDate.AddMonths(3);
                        duplicate.EndDate = duplicate.EndDate.AddMonths(-3);
                        break;
                    case ProblemType.DateStraddle:
                        duplicate.StartDate = duplicate.StartDate.AddMonths(-3);
                        duplicate.EndDate = duplicate.EndDate.AddMonths(3);
                        break;
                    default:
                        break;
                }
            }

            return duplicates;
        }

        /// <summary>
        /// Generates a set of datalocks for the input apprenticeships
        /// </summary>
        /// <param name="rows"></param>
        private static DataLockCollection CreateDataLockCollection(List<DataRow> rows)
        {
            var result = new DataLockCollection
            {
                PageNumber = 1,
                TotalNumberOfPages = 1
            };

            foreach (var row in rows)
            {
                var datalock = new DataLockStatus();

                var course = row.ProgType == "25" ?
                    row.StdCode :
                    $"{row.FworkCode}-{row.ProgType}-{row.PwayCode}";

                var pei = $"{course}-{row.StartDate.ToString("d")}"; //todo: also simulate 01/08/2018

                datalock.Id = DataHelper.GetNextDataLockEventId();
                datalock.ProcessDateTime = DateTime.UtcNow;
                datalock.IlrFileName = "ilr-submission.xml";
                datalock.Ukprn = 10000534;
                datalock.Uln = Int64.Parse(row.ULN);
                datalock.Status = "New"; //todo: simulate multiple events of diff. statuses
                datalock.LearnRefNumber = "1";
                datalock.AimSeqNumber = 1;
                datalock.PriceEpisodeIdentifier = pei;
                datalock.ApprenticeshipId = row.Id;
                datalock.EventSource = "Submission";
                datalock.IlrStartDate = row.StartDate; //todo: provide variance with below
                datalock.IlrProgrammeType = Int32.Parse(row.ProgType);
                datalock.IlrStandardCode = row.StdCode == null ? default(int?) : Int32.Parse(row.StdCode);
                datalock.IlrFrameworkCode = row.FworkCode == null ? default(int?) : Int32.Parse(row.FworkCode);
                datalock.IlrPathwayCode = row.PwayCode == null ? default(int?) : Int32.Parse(row.PwayCode);
                datalock.IlrTrainingPrice = row.TotalPrice * 2;
                datalock.IlrEndpointAssessorPrice = 0;
                datalock.IlrPriceEffectiveDate = row.StartDate;
                datalock.IlrPriceEffectiveFromDate = row.StartDate;
                datalock.Errors.Add(new DataLockError
                {
                    ErrorCode = "DLOCK_07",
                    SystemDescription = "Price datalock"
                });
                result.Items.Add(datalock);
            }

            //if programme type == 25, then include a std code, otherwise framework + prog type + pathway

            //                new TrainingCourse { FworkCode = "583", ProgType = "3", PwayCode = "4"},

            return result;
        }

        private static void OutputFile(List<DataRow> rows, string path)
        {
            var file = new StreamWriter(path);
            file.WriteLine("CohortRef,ULN,FamilyName,GivenNames,DateOfBirth,ProgType,FworkCode,PwayCode,StdCode,StartDate,EndDate,TotalPrice,EPAOrgID,ProviderRef");

            foreach (var row in rows)
            {
                var r = new StringBuilder();
                r.Append(row.CohortRef);
                r.Append(",");
                r.Append(row.ULN);
                r.Append(",");
                r.Append(row.FamilyName);
                r.Append(",");
                r.Append(row.GivenNames);
                r.Append(",");
                r.Append(row.DateOfBirth.ToString("yyy-MM-dd"));
                r.Append(",");
                r.Append(row.ProgType);
                r.Append(",");
                r.Append(row.FworkCode);
                r.Append(",");
                r.Append(row.PwayCode);
                r.Append(",");
                r.Append(row.StdCode);
                r.Append(",");
                r.Append(row.StartDate.ToString("yyy-MM"));
                r.Append(",");
                r.Append(row.EndDate.ToString("yyy-MM"));
                r.Append(",");
                r.Append(row.TotalPrice);
                r.Append(",");
                r.Append("");
                r.Append(",");
                r.Append("");

                file.WriteLine(r.ToString());
            }

            file.Close();
        }

    }
}
