using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Moldels;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Class {
    public class GenericsPersistence : IGenericsPersistence {

        private readonly ProEventosContext _context;

        public GenericsPersistence(ProEventosContext context) {
            _context = context;
        }
        public void Add<T>(T entity) where T : class {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class {
            _context.RemoveRange(entityArray);
        }
        public async Task<bool> SaveChangesAsync() {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
