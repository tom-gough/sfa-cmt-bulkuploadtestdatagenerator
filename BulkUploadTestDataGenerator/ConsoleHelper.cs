using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkUploadTestDataGenerator
{
    public static class ConsoleHelper
    {
        public static int ReadInteger(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);

                var input = Console.ReadLine();

                int parsed;

                if (int.TryParse(input, out parsed))
                {
                    return parsed;
                }

                Console.WriteLine("Error! Not a number");
            }
        }


    }
}
