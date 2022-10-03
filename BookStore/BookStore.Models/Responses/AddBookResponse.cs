using BookStore.Models.Models;

namespace BookStore.Models.Responses
{
    public class AddBookResponse : BaseResponse
    {
        public Book Book { get; set; }
    }
}
