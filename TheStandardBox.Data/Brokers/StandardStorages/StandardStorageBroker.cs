// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace TheStandardBox.Data.Brokers.StandardStorages
{
    public class StandardStorageBroker : EFxceptionsContext
    {
        protected readonly IConfiguration configuration;
        protected virtual string DefaultConnectionName => "DefaultConnection";

        public StandardStorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
                this.configuration.GetConnectionString(name: DefaultConnectionName);

            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder entityBuilder)
        {
            base.OnModelCreating(entityBuilder);
        }

        public override void Dispose()
        { }
    }

    public class StandardStorageBroker<TEntity> : StandardStorageBroker, IStandardStorageBroker<TEntity>
        where TEntity : class, IStandardEntity
    {
        public StandardStorageBroker(IConfiguration configuration)
             : base(configuration) { }

        public async ValueTask<TEntity> InsertEntityAsync(TEntity entity)
        {
            this.Entry(entity).State = EntityState.Added;
            await this.SaveChangesAsync();

            return entity;
        }

        public IQueryable<TEntity> SelectAllEntities() => this.Set<TEntity>();

        public async ValueTask<TEntity> SelectEntityByIdAsync(params object[] entityIds) =>
            await this.FindAsync<TEntity>(entityIds);

        public async ValueTask<TEntity> UpdateEntityAsync(TEntity entity)
        {
            this.Entry(entity).State = EntityState.Modified;
            await this.SaveChangesAsync();

            return entity;
        }

        public async ValueTask<TEntity> DeleteEntityAsync(TEntity entity)
        {
            this.Entry(entity).State = EntityState.Deleted;
            await this.SaveChangesAsync();

            return entity;
        }
    }
}