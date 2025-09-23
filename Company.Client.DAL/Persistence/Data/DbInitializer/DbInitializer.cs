using Company.Client.DAL.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.DAL.Persistence.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _context;

        public DbInitializer(AppDbContext context) //ask runtime for creating an instance of AppDbContext implicitly
        {
            _context = context;
        }
        public void Initialize()
        {
            if(_context.Database.GetPendingMigrations().Any())
                _context.Database.Migrate();
        }

        public void Seed()
        {
            //throw new NotImplementedException();
        }
    }
}
