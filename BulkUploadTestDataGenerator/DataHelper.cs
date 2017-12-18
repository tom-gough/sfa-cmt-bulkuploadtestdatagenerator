using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkUploadTestDataGenerator
{
    public static class DataHelper
    {
        private static readonly List<string> _firstNames;
        private static readonly List<string> _surnames;
        private static readonly List<string> _generatedULNs;
        private static readonly List<TrainingCourse> _trainingCourses;
        private static long _apprenticeshipId;
        private static long _dataLockEventId;
        static DataHelper()
        {
            _generatedULNs = new List<string>();

            _firstNames = new List<string>
            {
                "John", "Tom", "Dave", "Mark", "Chris", "Lee", "Susan", "Mary",
                "Jane", "James", "John", "Robert", "Michael", "David", "Charles", "Joseph",
                "Daniel", "Mary", "Patricia", "Linda", "Barbara", "Elizabeth", "Joshua", "Kevin",
                "Lee"
            };

            _surnames = new List<string>
            {
                "Smith",
                "Jones",
                "Cooper",
                "Froberg",
                "Williams",
                "Davis",
                "Carter",
                "Johnson",
                "Brown",
                "Miller",
                "Wilson",
                "Moore",
                "Tyler",
                "Jackson",
                "White",
                "Harris"
            };

            _trainingCourses = new List<TrainingCourse>
            {
                new TrainingCourse { FworkCode = "583", ProgType = "3", PwayCode = "4"},
                new TrainingCourse { FworkCode = "548", ProgType = "3", PwayCode = "1"},
                new TrainingCourse { FworkCode = "418", ProgType = "2", PwayCode = "1"},
                new TrainingCourse { ProgType = "25", StandardCode = "6" },
            };
        }

        public static string GetRandomFirstName()
        {
            return _firstNames.RandomElement();
        }

        public static string GetRandomLastName()
        {
            return _surnames.RandomElement();
        }

        public static string GetRandomUniqueULN()
        {
            while (true)
            {
                var uln = GenerateULN();
                if (uln.Length != 10) continue;
                if(!_generatedULNs.Contains(uln))
                {
                    _generatedULNs.Add(uln);
                    return uln;
                }
            }
        }

        private static string GenerateULN()
        {
            var result = new StringBuilder();

            var firstdigit = (RandomHelper.GetRandomNumber(8)+1).ToString();
            result.Append(firstdigit);

            for (var c = 0; c < 8; c++)
            {
                var digit = RandomHelper.GetRandomNumber(9).ToString();
                result.Append(digit);
            }

            var checkdigit = CalculateUlnChecksum(result.ToString());

            return result + checkdigit;
        }

        private static string CalculateUlnChecksum(string partialUln)
        {
            var ulnCheckArray = partialUln.ToCharArray()
                                    .Select(c => long.Parse(c.ToString()))
                                    .ToList();

            var multiplier = 10;
            long sumOfDigits = 0;
            for (var i = 0; i < 9; i++)
            {
                sumOfDigits += ulnCheckArray[i] * multiplier;
                multiplier--;
            }

            var remainder = (505 - sumOfDigits);

            var checkDigit = remainder % 11;

            return checkDigit.ToString();
        }

        public static DateTime GetRandomDateOfBirth()
        {
            var year = 1960 + RandomHelper.GetRandomNumber(40);
            var month = RandomHelper.GetRandomNumber(11) + 1;
            var day = RandomHelper.GetRandomNumber(27) + 1;

            return new DateTime(year,month,day);
        }

        public static DateTime GetRandomStartDate()
        {
            var result = DateTime.Now;
            result = result.AddMonths(RandomHelper.GetRandomNumber(6));
            return new DateTime(result.Year, result.Month, 1);
        }

        public static DateTime GetRandomEndDate(DateTime startDate)
        {
            var duration = RandomHelper.GetRandomNumber(3) + 9;
            return startDate.AddMonths(duration);
        }

        public static ProblemType GetRandomProblemType()
        {
            var t = RandomHelper.GetRandomNumber(3);
            return (ProblemType) t;
        }

        public static TrainingCourse GetRandomTrainingCourse()
        {
            return _trainingCourses.RandomElement();
        }

        public static long GetNextApprenticeshipId()
        {
            _apprenticeshipId++;
            return _apprenticeshipId;
        }

        public static long GetNextDataLockEventId()
        {
            _dataLockEventId++;
            return _dataLockEventId;
        }
    }
}
