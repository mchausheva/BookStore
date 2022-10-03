using BookStore.Models.Models;

namespace BookStore.Models.Responses
{
    public  class AddPersonResponse : BaseResponse
    {
        public Person Person { get; set; }
    }
}
