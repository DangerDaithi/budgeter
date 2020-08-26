using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Budgeter
{
    public class ExpenditureReportFileWriter
    {
        public string BasePath { get { return "ExpenditureReports/"; } }
        public string BaseFileName { get { return "ExpenditureReport-"; } }

        public void Save(string content)
        {
            var fileNameSuffix = DateTime.Now.ToString("dd-MM-yyyy hh-MM-ss");
            var fullFilePath = $"{BasePath}/{BaseFileName}{fileNameSuffix}.txt";
            if (File.Exists(fullFilePath))
            {
                // todo add logging
                Console.WriteLine($"File {fullFilePath} already exists, overwriting.");
            }

            if (!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }

            File.WriteAllLines(fullFilePath, new[] { content }, Encoding.UTF8);
        }
    }
}
