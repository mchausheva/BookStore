using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Users;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace BookStore.DL.Repositories
{
    public class EmployeeRepository : IEmployeeRpository
    {
        private readonly ILogger<EmployeeRepository> _logger;
        private readonly IConfiguration _configuration;
        public EmployeeRepository(ILogger<EmployeeRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Employee>> GetEmployeeDetails()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Employee>("SELECT * FROM Employee WITH(NOLOCK)");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetEmployeeDetails)} - {ex.Message}", ex);
            }
            return Enumerable.Empty<Employee>();
        }

        public async Task<Employee?> GetEmployeeDetails(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM Employee WITH(NOLOCK) WHERE EmployeeId = @EmployeeId", new { EmployeeId = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetEmployeeDetails)} - {ex.Message}", ex);
            }
            return new Employee();
        }

        public async Task AddEmployee(Employee employee)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    string query =
                       @"INSERT INTO Employee 
                        ([EmployeeID]
                       ,[NationalIDNumber]
                       ,[EmployeeName]
                       ,[LoginID]
                       ,[JobTitle]
                       ,[BirthDate]
                       ,[MaritalStatus]
                       ,[Gender]
                       ,[HireDate]
                       ,[VacationHours]
                       ,[SickLeaveHours]
                       ,[rowguid]
                       ,[ModifiedDate])
                        VALUES 
                        (@EmployeeID
                       ,@NationalIDNumber
                       ,@EmployeeName
                       ,@LoginID
                       ,@JobTitle
                       ,@BirthDate
                       ,@MaritalStatus
                       ,@Gender
                       ,@HireDate
                       ,@VacationHours
                       ,@SickLeaveHours
                       ,@rowguid
                       ,@ModifiedDate)";
                    var result = await conn.ExecuteAsync(query, employee);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddEmployee)} - {ex.Message}", ex);
            }
        }

        public async Task<bool> CheckEmployee(int id)
        {
            return (await GetEmployeeDetails(id) != null);
        }

        public async Task DeleteEmployee(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<Author>("DELETE FROM Employee WHERE EmployeeId = @EmployeeId", new { EmployeeId = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteEmployee)} - {ex.Message}", ex);
            }
        }
        
        public async Task UpdateEmployee(Employee employee)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    string query =
                      @"UPDATE [dbo].[Employee]
                       SET [EmployeeID] = @EmployeeID
                          ,[NationalIDNumber] = @NationalIDNumber
                          ,[EmployeeName] = @EmployeeName
                          ,[LoginID] = @LoginID
                          ,[JobTitle] = @JobTitle
                          ,[BirthDate] = @BirthDate
                          ,[MaritalStatus] = @MaritalStatus
                          ,[Gender] = @Gender
                          ,[HireDate] = @HireDate
                          ,[VacationHours] = @VacationHours
                          ,[SickLeaveHours] = @SickLeaveHours
                          ,[rowguid] = @rowguid
                          ,[ModifiedDate] = @ModifiedDate
                     WHERE EmployeeID = @EmployeeID";

                    var result = await conn.ExecuteAsync(query, employee);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateEmployee)} - {ex.Message}", ex);
            }
        }
    }
}
