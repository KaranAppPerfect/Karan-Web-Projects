using Microsoft.EntityFrameworkCore;

namespace AspnetCoreCrudApp.Data
{

    public class ApplicationDbContext: DbContext{

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){
                
            }

    }


}