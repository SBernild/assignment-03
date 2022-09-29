using Assignment.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Configuration;

public abstract class TestConnection{
        private DbContext _context;
        public TestConnection(){
            /* DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<KabanContext>();
            optionsBuilder.UseSqlServer("Server=localhost; Database=Master; User Id=SA; Awes0mepassword!; Trusted_Connection=False;Encrypt=False");
            DbContextOptions options = optionsBuilder.Options;

            _context = new KabanContext(options); */
            var optionsBuilder = new DbContextOptionsBuilder<KabanContext>();
            
            optionsBuilder.UseSqlServer("Server=localhost; Database=Master; User Id=SA; Password=Awes0mepassword!; Encrypt=False");
            
            var options = optionsBuilder.Options;
            
             _context = new KabanContext(options);
            
        }
 
}

