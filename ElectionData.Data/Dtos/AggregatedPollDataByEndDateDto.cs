using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionData.Data.Dtos
{
    public class AggregatedPollDataByEndDateDto 
    {
        public DateOnly EndDate { get; set; } 
        public decimal TrumpAverage { get; set; }
        public decimal HarrisAverage { get; set; }
    }
}
