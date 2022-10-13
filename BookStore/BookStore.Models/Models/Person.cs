using MessagePack;

namespace BookStore.Models.Models
{
    [MessagePackObject]
    public record Person
    {
        [Key(0)]
        public int Id { get; init; }
        [Key(1)]
        public string FirstName { get; init; }
        [Key(2)]
        public string LastName { get; init; }
        [Key(3)]
        public int Age { get; init; }
        [Key(4)]
        public DateTime DateOfBirth { get; init; }
    }
}