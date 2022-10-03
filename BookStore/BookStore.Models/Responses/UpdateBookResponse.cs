using BookStore.Models.Models;

namespace BookStore.Models.Responses
{
    public class UpdateBookResponse : BaseResponse
    {
        public Book Book { get; set; }
    }
}
