using System.ComponentModel.DataAnnotations;

namespace ElectionData.Data.Entities
{
    public class CleanPoll
    {
        [Key]
        public int Id { get; set; }

        public string PollLink { get; set; }

        [Required]
        public string Pollster { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public int? SampleSize { get; set; }

        public string? SampleType { get; set; }

        public decimal? MoE { get; set; }

        public decimal? Trump { get; set; }

        public decimal? Harris { get; set; }

        public string? Spread { get; set; }
    }
}
