using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HospitalSystem.Infrastructure.Persistence;

public sealed class SchedulingDbContextFactory : IDesignTimeDbContextFactory<SchedulingDbContext>
{
    public SchedulingDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<SchedulingDbContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HospitalSystem_Scheduling;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true")
            .Options;

        return new SchedulingDbContext(options);
    }
}
