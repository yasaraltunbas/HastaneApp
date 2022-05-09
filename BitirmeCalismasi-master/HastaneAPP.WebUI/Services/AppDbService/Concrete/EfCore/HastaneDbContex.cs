using System.IO;
using HastaneAPP.WebUI.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HastaneAPP.WebUI.Services.AppDbService.Concrete.EfCore
{
    public class HastaneDbContex : DbContext
    {
        public DbSet<Hastalar> Hastalars { get; set; }
        public DbSet<Operasyonlar> Operasyonlars { get; set; }
        public DbSet<ExtraHastalik> extraHastaliks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .Build();

            var connectionString = configuration.GetConnectionString("ProjectDBConnection");

            optionsBuilder.UseMySQL(connectionString);
        }


    }
}