using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionData.Data.Dtos
{
    public class PollDto
    {
        public int Id { get; set; }
        public string PollLink { get; set; } = string.Empty;
        public string Pollster { get; set; } = string.Empty;
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? SampleSize { get; set; }
        public string? SampleType { get; set; }
        public decimal? MoE { get; set; }
        public decimal? Trump { get; set; }
        public decimal? Harris { get; set; }
        public string? Spread { get; set; }
    }
}
