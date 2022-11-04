using Microsoft.EntityFrameworkCore;
using TheStandardBox.Data.Brokers.StandardStorages;
using WebApplication1.Models.Foundations.Options;
using WebApplication1.Models.Foundations.Users;

namespace WebApplication1.Brokers.Storages
{
    public partial class StorageBroker : StandardStorageBroker
    {
        private static void AddUserOptionConfigurations(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasMany(e => e.Options);

            modelBuilder.Entity<Option>()
               .HasMany(e => e.Users);
        }
    }
}