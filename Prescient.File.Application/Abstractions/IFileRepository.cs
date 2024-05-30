using Prescient.File.Domain.Models;
using Prescient.File.Domain.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescient.File.Application.Abstractions
{
    public interface IFileRepository
    {
        List<DailyMtm> ProcessFile(string FilePath);
        bool SaveFile(List<DailyMtm>  filieList);
        Task DownloadFileAsync(string url, string localFilePath);
    }
}
