using Module06MVVM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.IO;

namespace Module06MVVM.Services
{
    public class DatabaseContext:DbContext
    {
        public DbSet<EmployeeModel> Finals { get; set; }
        public DatabaseContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Finals.db");
            optionsBuilder.UseSqlite($"Filename={dbpath}");
        }
    }
}
