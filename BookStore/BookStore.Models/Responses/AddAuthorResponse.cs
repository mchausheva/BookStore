using BookStore.Models.Models;

namespace BookStore.Models.Responses
{
    public class AddAuthorResponse : BaseResponse
    {
        public Author Author { get; set; }
    }
}
