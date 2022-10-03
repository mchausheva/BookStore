using BookStore.Models.Models;

namespace BookStore.Models.Responses
{
    public class UpdateAuthorResponse : BaseResponse
    {
        public Author Author { get; set; }
    }
}
