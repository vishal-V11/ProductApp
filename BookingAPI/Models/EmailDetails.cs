namespace BookingAPI.Models
{
    public class EmailDetails
    {
        public string? To { get; set; } = string.Empty;
        public string? Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;
    }
}
