// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheStandardBox.Core.Models.Foundations.Bases;
using TheStandardBox.Data.Services.Foundations.Standards;

namespace TheStandardBox.Data.Controllers
{
    public partial class StandardController<TEntity> : CatchableController<TEntity>
        where TEntity : class, IBaseEntity
    {
        protected readonly IStandardService<TEntity> standardService;

        public TEntity Entity { get; protected set; }

        public StandardController(IStandardService<TEntity> standardService) =>
             this.standardService = standardService;

        [HttpPost]
        public virtual ValueTask<ActionResult<TEntity>> PostEntityAsync(TEntity entity) =>
            TryCatchOnPost(async () =>
            {
                return await this.standardService.AddEntityAsync(entity);
            });

        [HttpGet]
        public virtual ActionResult<IQueryable<TEntity>> GetAllEntities() =>
            TryCatchOnGetAll(() =>
            {
                return this.standardService.RetrieveAllEntities();
            });


        [HttpGet("{entityId}")]
        public virtual ValueTask<ActionResult<TEntity>> GetEntityByIdAsync(Guid entityId) =>
            TryCatchOnGetById(async () =>
            {
                return await this.standardService.RetrieveEntityByIdAsync(entityId);
            });

        [HttpGet("{entityId1}/{entityId2}")]
        public virtual ValueTask<ActionResult<TEntity>> GetEntityByIdsAsync(Guid entityId1, Guid entityId2) =>
           TryCatchOnGetById(async () =>
           {
               return await this.standardService.RetrieveEntityByIdAsync(entityId1, entityId2);
           });

        [HttpPut]
        public virtual ValueTask<ActionResult<TEntity>> PutEntityAsync(TEntity entity) =>
            TryCatchOnPut(async () =>
            {
                return await this.standardService.ModifyEntityAsync(entity);
            });

        [HttpDelete("{itemId}")]
        public virtual ValueTask<ActionResult<TEntity>> DeleteEntityByIdAsync(Guid itemId) =>
            TryCatchOnDelete(async () =>
            {
                return await this.standardService.RemoveEntityByIdAsync(itemId);
            });
    }
}