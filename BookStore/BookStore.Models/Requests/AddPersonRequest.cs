namespace BookStore.Models.Requests
{
    public class AddPersonRequest
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public int Age { get; init; }
        public DateTime DateOfBirth { get; init; }
    }
}
