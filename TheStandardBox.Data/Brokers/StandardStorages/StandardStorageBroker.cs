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
    public class StandardStorageBroker : EFxceptionsContext, IStandardStorageBroker
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

        public async ValueTask<TEntity> InsertEntityAsync<TEntity>(TEntity entity)
             where TEntity : class, IStandardEntity
        {
            this.Entry(entity).State = EntityState.Added;
            await this.SaveChangesAsync();

            return entity;
        }

        public virtual IQueryable<TEntity> SelectAllEntities<TEntity>()
             where TEntity : class, IStandardEntity => this.Set<TEntity>();

        public virtual async ValueTask<TEntity> SelectEntityByIdAsync<TEntity>(
            params object[] entityIds) where TEntity : class, IStandardEntity
        {
            return await this.FindAsync<TEntity>(entityIds);
        }

        public virtual async ValueTask<TEntity> UpdateEntityAsync<TEntity>(TEntity entity)
            where TEntity : class, IStandardEntity
        {
            this.Entry(entity).State = EntityState.Modified;
            await this.SaveChangesAsync();

            return entity;
        }

        public virtual async ValueTask<TEntity> DeleteEntityAsync<TEntity>(TEntity entity)
            where TEntity : class, IStandardEntity
        {
            this.Entry(entity).State = EntityState.Deleted;
            await this.SaveChangesAsync();

            return entity;
        }

        public override void Dispose()
        { }
    }
}