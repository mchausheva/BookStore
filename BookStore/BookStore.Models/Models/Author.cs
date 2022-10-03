namespace BookStore.Models.Models
{
    public record Author
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Age { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string Nickname { get; init; }
    }
}
