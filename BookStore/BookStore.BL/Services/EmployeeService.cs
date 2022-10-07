using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models.Users;
using Microsoft.Extensions.Logging;

namespace BookStore.BL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRpository _employeeRpository;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly ILogger<EmployeeService> _logger;
        public EmployeeService(IEmployeeRpository employeeRepository, IUserInfoRepository userInfoRepository, ILogger<EmployeeService> logger)
        {
            _employeeRpository = employeeRepository;
            _userInfoRepository = userInfoRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>> GetEmployeeDetails()
        {
            return await _employeeRpository.GetEmployeeDetails();
        }

        public async Task<Employee?> GetEmployeeDetails(int id)
        {
            return await _employeeRpository.GetEmployeeDetails(id);
        }

        public Task<UserInfo?> GetUserInfoAsync(string email, string password)
        {
            return _userInfoRepository.GetUserInfoAsync(email, password);
        }

        public async Task AddEmployee(Employee employee)
        {
            await _employeeRpository.AddEmployee(employee);
        }

        public async Task<bool> CheckEmployee(int id)
        {
            return await _employeeRpository.CheckEmployee(id);
        }

        public async Task DeleteEmployee(int id)
        {
            await _employeeRpository.DeleteEmployee(id);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await _employeeRpository.UpdateEmployee(employee);
        }
    }
}
