using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Passwords.Model 
{
    public partial class PasswordsDb : DbContext
    {
        public PasswordsDb() { }

        public PasswordsDb(DbContextOptions<PasswordsDb> options)
            : base(options) 
        { 
        
        }

        public virtual DbSet<Code> Codes { get; set; } = null!;
        public virtual DbSet<Data> Data { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
