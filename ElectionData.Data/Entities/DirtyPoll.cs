namespace ElectionData.Data.Entities
{
    public class DirtyPoll
    {
        public string Pollster { get; set; }
        public string Date { get; set; }
        public string? Sample { get; set; }
        public string? MoE { get; set; }
        public string Trump { get; set; }
        public string Harris { get; set; }
        public string? Spread { get; set; }
        public string? PollLink { get; set; }
    }
}
