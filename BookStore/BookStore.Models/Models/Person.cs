namespace BookStore.Models.Models
{
    public record Person
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public int Age { get; init; }
        public DateTime DateOfBirth { get; init; }
    }
}