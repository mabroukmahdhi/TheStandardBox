using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheStandardBox.Data.Brokers.StandardStorages;
using WebApplication1.Models.Foundations.Options;
using WebApplication1.Models.Foundations.Users;

namespace WebApplication1.Brokers.Storages
{
    public partial class StorageBroker : StandardStorageBroker
    {
        public StorageBroker(IConfiguration configuration)
            : base(configuration)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AddUserOptionConfigurations(modelBuilder);
        }

        public DbSet<Option> Options { get; set; }
        public DbSet<User> Users { get; set; }
    }
}