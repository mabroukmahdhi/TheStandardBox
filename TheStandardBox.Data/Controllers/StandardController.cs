// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.Core.Models.Foundations.Standards.Exceptions;
using TheStandardBox.Data.Services.Foundations.Standards;

namespace TheStandardBox.Data.Controllers
{
    public partial class StandardController<TEntity> : RESTFulController
        where TEntity : class, IStandardEntity
    {
        protected readonly IStandardService<TEntity> standardService;

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


        [HttpGet("{itemId}")]
        public virtual ValueTask<ActionResult<TEntity>> GetEntityByIdAsync(Guid itemId) =>
            TryCatchOnGetById(async () =>
            {
                return await this.standardService.RetrieveEntityByIdAsync(itemId);
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