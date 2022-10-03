namespace BookStore.Models.Requests
{
    public class AddAuthorRequest
    {
        public string Name { get; init; }
        public int Age { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string Nickname { get; init; }
    }
}
