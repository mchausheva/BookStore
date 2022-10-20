using MessagePack;

namespace BookStore.Models.Models.Users
{
    [MessagePackObject]
    public record Purchase
    {
        [Key(0)]
        public Guid Id { get; set; }
        [Key(1)]
        public int UserId { get; set; }
        [Key(2)]
        public IEnumerable<Book> Books { get; set; }
        [Key(3)]
        public decimal TotalMoney { get; set; }
        [Key(4)]
        public IEnumerable<string> AdditionalInfo { get; set; } = Enumerable.Empty<string>();
    }
}
