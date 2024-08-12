using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Portal.Persistence.Context;

namespace Portal.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PortalDbContext>
    {
        public PortalDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<PortalDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer("Server=DESKTOP-RSDLUJ4;Database=PortalDb;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");
            return new PortalDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
