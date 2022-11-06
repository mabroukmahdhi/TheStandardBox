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
using TheStandardBox.Core.Models.Foundations.Bases;
using TheStandardBox.Core.Models.Foundations.Joins;

namespace TheStandardBox.Data.Brokers.StandardStorages
{
    public class StandardStorageBroker : EFxceptionsContext, IStandardStorageBroker
    {
        protected readonly IConfiguration configuration;
        protected virtual string DefaultConnectionName => "DefaultConnection";
        protected virtual bool UsesNoTrackingBehavior => true;

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

            if (UsesNoTrackingBehavior)
            {
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

        public virtual async ValueTask<TEntity> InsertEntityAsync<TEntity>(TEntity entity)
             where TEntity : class, IBaseEntity
        {
            this.Entry(entity).State = EntityState.Added;
            await this.SaveChangesAsync();

            return entity;
        }

        public virtual IQueryable<TEntity> SelectAllEntities<TEntity>()
             where TEntity : class, IBaseEntity => this.Set<TEntity>();

        public virtual async ValueTask<TEntity> SelectEntityByIdAsync<TEntity>(
            params object[] entityIds) where TEntity : class, IBaseEntity
        {
            return await this.FindAsync<TEntity>(entityIds);
        }

        public virtual async ValueTask<TEntity> UpdateEntityAsync<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity
        {
            this.Entry(entity).State = EntityState.Modified;
            await this.SaveChangesAsync();

            return entity;
        }

        public virtual async ValueTask<TEntity> DeleteEntityAsync<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity
        {
            this.Entry(entity).State = EntityState.Deleted;
            await this.SaveChangesAsync();

            return entity;
        }

        protected virtual void SetJoinEntityReferences<TJoinEntity, TRelatedEntity1, TRelatedEntity2>(
            ModelBuilder modelBuilder) where TJoinEntity : class, IJoinEntity
        {
            modelBuilder.Entity<TJoinEntity>()
                .HasKey(typeof(TRelatedEntity1).Name + "Id", typeof(TRelatedEntity2).Name + "Id");

            modelBuilder.Entity<TJoinEntity>()
                .HasOne(typeof(TRelatedEntity1).Name)
                .WithMany(typeof(TRelatedEntity2).Name + "s")
                .HasForeignKey(typeof(TRelatedEntity1).Name + "Id")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TJoinEntity>()
                .HasOne(typeof(TRelatedEntity2).Name)
                .WithMany(typeof(TRelatedEntity1).Name + "s")
                .HasForeignKey(typeof(TRelatedEntity2).Name + "Id")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void Dispose()
        { }
    }
}