using Microsoft.EntityFrameworkCore;

namespace modpackFront.Models
{
    public partial class ModPackContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration Config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(Config.GetConnectionString("modpack"));
            }
        }
    }
}