namespace BookStore.Models.Configurations
{
    public class KafkaSettings
    {
        public string BootstrapServers { get; set; }
        public int AutoOffsetReset { get; set; }
        public string GroupId { get; set; }
        public KafkaSettings()
        {
        }
    }
}
