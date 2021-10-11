using Api_BigchainDb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api_BigchainDb.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task<User> CreateAsync(User book);
        Task UpdateAsync(string id, User book);
        Task DeleteAsync(string id);
    }

    public interface IBookService
    {
        Task<List<Books>> GetAllAsync();
    }
}
