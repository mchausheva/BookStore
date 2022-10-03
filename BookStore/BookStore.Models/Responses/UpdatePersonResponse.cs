using BookStore.Models.Models;

namespace BookStore.Models.Responses
{
    public class UpdatePersonResponse : BaseResponse
    {
        public Person Person { get; set; }
    }
}
