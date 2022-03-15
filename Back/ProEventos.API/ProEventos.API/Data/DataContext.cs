﻿using Microsoft.EntityFrameworkCore;
using ProEventos.API.Moldels;

namespace ProEventos.API.Data {
    public class DataContext : DbContext{
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<Evento> Evento { get; set; }
    }
}
