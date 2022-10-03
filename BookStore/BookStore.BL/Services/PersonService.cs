using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BookStore.BL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;
        public PersonService(IPersonRepository personService, IMapper mapper, ILogger<PersonService> logger)
        {
            _personRepository = personService;
            _mapper = mapper;
            _logger = logger;
        }
        public IEnumerable<Person?> GetAllPeople()
        {
            _logger.LogInformation("Success");
            return _personRepository.GetAllPeople();
        }
        public Person GetById(int id)
        {
            return _personRepository.GetById(id);
        }
        public AddPersonResponse AddPerson(AddPersonRequest personRequest)
        {
            try
            {
                var personExist = _personRepository.GetByName(personRequest.FirstName);
                if (personExist != null)
                    return new AddPersonResponse()
                    {
                        Person = personExist,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "This Person Already Exist!"
                    };

                var person = _mapper.Map<Person>(personRequest);
                var result = _personRepository.AddPerson(person);

                return new AddPersonResponse()
                {
                    Person = result,
                    HttpStatusCode = HttpStatusCode.OK,
                    Message = "Successfully Added Person"
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"The person can not be add");
                throw;
            }

        }
        public Person DeletePersonById(int id)
        {
            _logger.LogInformation("Success");
            return _personRepository.DeletePersonById(id);
        }
        public UpdatePersonResponse UpdatePerson(UpdatePersonRequest personRequest)
        {
            try
            {
                var personExist = _personRepository.GetByName(personRequest.LastName);
                if (personExist == null)
                    return new UpdatePersonResponse()
                    {
                        Person = personExist,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Not Updated"
                    };

                var person = _mapper.Map<Person>(personExist);
                var result = _personRepository.UpdatePerson(person);

                return new UpdatePersonResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Person = result,
                    Message = "Successfully Updated Person"
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"The person can not be update");
                throw;
            }
        }
    }
}