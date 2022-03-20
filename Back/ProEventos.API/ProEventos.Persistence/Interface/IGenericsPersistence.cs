﻿using ProEventos.Domain.Moldels;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interface {
    public interface IGenericsPersistence {
        
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entity) where T : class;
        Task<bool> SaveChangesAsync();

      
    }
}
