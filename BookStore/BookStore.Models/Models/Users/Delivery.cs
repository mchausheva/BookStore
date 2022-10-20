using MessagePack;

namespace BookStore.Models.Models.Users
{
    [MessagePackObject]
    public record Delivery
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public Book Book { get; set; }
        [Key(2)]
        public int Quantity { get; set; }
    }
}
