using Prescient.File.Application.Abstractions;
using Prescient.File.Domain.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using Prescient.File.Domain.Models.Dtos;
using Prescient.File.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Xml;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Prescient.File.Data.Repository.FileRepo
{
    public class FileRepository : IFileRepository
    {
        private readonly ILogger<FileRepository> _logger;
        private readonly HttpClient _client;
       

        public FileRepository(ILogger<FileRepository> logger, HttpClient client)
        {
            _logger = logger;
            _client = client;
        }   

        public  List<DailyMtm> ProcessFile(string filePath)
        {
            try
            {
                List<DailyMtm> dailyMtms = new List<DailyMtm>();
                // Create an instance of Excel
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                Workbook workbook = excelApp.Workbooks.Open(filePath);
                Worksheet worksheet = (Worksheet)workbook.Sheets[1];

                // Get the used range of the worksheet
                Microsoft.Office.Interop.Excel.Range usedRange = worksheet.UsedRange;

                // Skip the header row
                for (int row = 6; row <= usedRange.Rows.Count; row++)
                {
                    DailyMtm data = new DailyMtm
                    {
                        FileDate = new DateOnly(),
                        Contract = (string)usedRange.Cells[row, 1],
                        ExpiryDate = (DateOnly)usedRange.Cells[row, 3],
                        Classification = (string)usedRange.Cells[row, 4],
                        Strike = (double)usedRange.Cells[row, 3],
                        CallPut = (string)usedRange.Cells[row, 3],
                        MTMYield = (double)usedRange.Cells[row, 3],
                        MarkPrice = (double)usedRange.Cells[row, 3],
                        SpotRate = (double)usedRange.Cells[row, 3],
                        PreviousMTM = (double)usedRange.Cells[row, 3],
                        PreviousPrice = (double)usedRange.Cells[row, 3],
                        PremiumOnOption = (double)usedRange.Cells[row, 3],
                        Volatility = (double)usedRange.Cells[row, 3],
                        Delta = (double)usedRange.Cells[row, 3],
                        DeltaValue = (double)usedRange.Cells[row, 3],
                        ContractsTraded = (double)usedRange.Cells[row, 3],
                        OpenInterest = (double)usedRange.Cells[row, 3],
                    };

                    dailyMtms.Add(data);
                }

                // Close the workbook and quit Excel
                workbook.Close();
                excelApp.Quit();
                _logger.LogInformation("File proccessed",dailyMtms);
                return dailyMtms;
            }
            catch (Exception ex)
            {
                return null;
                Console.WriteLine("Error: ", ex.Message);
            }


        }

        public bool SaveFile(List<DailyMtm> filieList)
        {
            try
            {
                bool success = false;

                using (var context = new ApplicationDbContext())
                {
                    context.BulkInsert(filieList);
                    success = true;
                }
                return success;
                _logger.LogInformation("File saved");
            }
            catch (Exception ex)
            {
                return false;
                Console.WriteLine("Error: ", ex.Message);
            }
        }

        public  async Task DownloadFileAsync(string url, string localFilePath)
        {
            try
            {
                // Check if the file already exists and the download is skipped
                if (System.IO.File.Exists(localFilePath))
                {
                    Console.WriteLine($"File '{localFilePath}' already exists. Skipping download.");
                    return;
                }
                else
                {
                    
                        using (var response = await _client.GetAsync(url))
                        {
                            response.EnsureSuccessStatusCode();

                            using (var stream = await response.Content.ReadAsStreamAsync())
                            {
                                using (var fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                                {
                                    await stream.CopyToAsync(fileStream);
                                }
                            }
                        }
                  
                }
                _logger.LogInformation("File downloaded");

            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error: ", ex.Message);
            }

        }

      
    }
}
