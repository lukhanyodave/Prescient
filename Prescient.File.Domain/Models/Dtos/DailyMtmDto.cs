using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescient.File.Domain.Models.Dtos
{
    public class DailyMtmDto
    {
        public DateOnly FileDate { get; set; }
        public string Contract { get; set; } = string.Empty;//A
        public DateOnly ExpiryDate { get; set; }//c
        public string Classification { get; set; } = string.Empty;//d
        public double Strike { get; set; }//column e
        public string CallPut { get; set; } = string.Empty;
        public double MTMYield { get; set; }
        public double MarkPrice { get; set; }
        public double SpotRate { get; set; }
        public double PreviousMTM { get; set; }
        public double PreviousPrice { get; set; }
        public double PremiumOnOption { get; set; }
        public double Volatility { get; set; }
        public double Delta { get; set; }
        public double DeltaValue { get; set; }
        public double ContractsTraded { get; set; }
        public double OpenInterest { get; set; }
    }
}
