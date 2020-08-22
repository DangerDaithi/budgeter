using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Budgeter
{
    public class ExpenditureReportFileWriter
    {
        private readonly string _basePath = "ExpenditureReports/";
        private readonly string _baseFileName = "ExpenditureReport-";

        public void Save(string content)
        {
            var fileNameSuffix = DateTime.Now.ToString("dd-MM-yyyy hh-MM-ss");
            var fullFilePath = $"{_basePath}/{_baseFileName}{fileNameSuffix}.txt";
            if (File.Exists(fullFilePath))
            {
                // todo add logging
                Console.WriteLine($"File {fullFilePath} already exists, overwriting.");
            }

            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }

            File.WriteAllLines(fullFilePath, new[] { content }, Encoding.UTF8);
        }
    }
}
