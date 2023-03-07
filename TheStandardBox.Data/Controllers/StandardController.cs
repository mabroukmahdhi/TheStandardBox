// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheStandardBox.Core.Models.Foundations.Bases;
using TheStandardBox.Data.Services.Foundations.Standards;

namespace TheStandardBox.Data.Controllers
{
    public partial class StandardController<TEntity> : CatchableController<TEntity>
        where TEntity : class, IBaseEntity
    {
        protected readonly IStandardService<TEntity> standardService;

        public StandardController(IStandardService<TEntity> standardService) =>
             this.standardService = standardService;

        [HttpPost]
        public virtual ValueTask<ActionResult<TEntity>> PostEntityAsync([FromBody] TEntity entity) =>
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
        public virtual ValueTask<ActionResult<TEntity>> PutEntityAsync([FromBody] TEntity entity) =>
            TryCatchOnPut(async () =>
            {
                return await this.standardService.ModifyEntityAsync(entity);
            });

        [HttpDelete("{entityId}")]
        public virtual ValueTask<ActionResult<TEntity>> DeleteEntityByIdAsync(Guid entityId) =>
            TryCatchOnDelete(async () =>
            {
                return await this.standardService.RemoveEntityByIdAsync(entityId);
            });
    }
}