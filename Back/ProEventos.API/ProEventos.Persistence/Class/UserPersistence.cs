using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Class {
    public class UserPersistence : GenericsPersistence, IUserPersistence {
        private readonly ProEventosContext _context;
        public UserPersistence(ProEventosContext context) : base(context)  {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUserAsync() {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id) {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username) {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}
