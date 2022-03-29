
using ProEventos.Domain.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interface {
    public interface IUserPersistence : IGenericsPersistence{
        Task<IEnumerable<User>> GetUserAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);

    }
}
