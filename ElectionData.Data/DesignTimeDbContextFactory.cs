using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionData.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ElectionDataDbContext>
    {
        public ElectionDataDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.appSettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ElectionDataDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseNpgsql(connectionString);

            return new ElectionDataDbContext(builder.Options);
        }
    }
}
