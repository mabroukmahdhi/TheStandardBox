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
    public class StandardController<TEntity> : RESTFulController
        where TEntity : class, IStandardEntity
    {
        protected readonly IStandardService<TEntity> standardService;

        public StandardController(IStandardService<TEntity> standardService) =>
            this.standardService = standardService;

        [HttpPost]
        public virtual async ValueTask<ActionResult<TEntity>> PostEntityAsync(TEntity entity)
        {
            try
            {
                TEntity addedEntity =
                    await this.standardService.AddEntityAsync(entity);

                return Created(addedEntity);
            }
            catch (EntityValidationException entityValidationException)
            {
                return BadRequest(entityValidationException.InnerException);
            }
            catch (EntityDependencyValidationException entityValidationException)
                when (entityValidationException.InnerException is InvalidEntityReferenceException)
            {
                return FailedDependency(entityValidationException.InnerException);
            }
            catch (EntityDependencyValidationException entityDependencyValidationException)
               when (entityDependencyValidationException.InnerException is AlreadyExistsEntityException)
            {
                return Conflict(entityDependencyValidationException.InnerException);
            }
            catch (EntityDependencyException entityDependencyException)
            {
                return InternalServerError(entityDependencyException);
            }
            catch (EntityServiceException standardServiceException)
            {
                return InternalServerError(standardServiceException);
            }
        }

        [HttpGet]
        public virtual ActionResult<IQueryable<TEntity>> GetAllEntities()
        {
            try
            {
                IQueryable<TEntity> retrievedEntities =
                    this.standardService.RetrieveAllEntities();

                return Ok(retrievedEntities);
            }
            catch (EntityDependencyException entityDependencyException)
            {
                return InternalServerError(entityDependencyException);
            }
            catch (EntityServiceException standardServiceException)
            {
                return InternalServerError(standardServiceException);
            }
        }

        [HttpGet("{itemId}")]
        public virtual async ValueTask<ActionResult<TEntity>> GetEntityByIdAsync(Guid itemId)
        {
            try
            {
                TEntity entity = await this.standardService.RetrieveEntityByIdAsync(itemId);

                return Ok(entity);
            }
            catch (EntityValidationException entityValidationException)
                when (entityValidationException.InnerException is NotFoundEntityException)
            {
                return NotFound(entityValidationException.InnerException);
            }
            catch (EntityValidationException entityValidationException)
            {
                return BadRequest(entityValidationException.InnerException);
            }
            catch (EntityDependencyException entityDependencyException)
            {
                return InternalServerError(entityDependencyException);
            }
            catch (EntityServiceException standardServiceException)
            {
                return InternalServerError(standardServiceException);
            }
        }

        [HttpPut]
        public virtual async ValueTask<ActionResult<TEntity>> PutEntityAsync(TEntity entity)
        {
            try
            {
                TEntity modifiedEntity =
                    await this.standardService.ModifyEntityAsync(entity);

                return Ok(modifiedEntity);
            }
            catch (EntityValidationException entityValidationException)
                when (entityValidationException.InnerException is NotFoundEntityException)
            {
                return NotFound(entityValidationException.InnerException);
            }
            catch (EntityValidationException entityValidationException)
            {
                return BadRequest(entityValidationException.InnerException);
            }
            catch (EntityDependencyValidationException entityValidationException)
                when (entityValidationException.InnerException is InvalidEntityReferenceException)
            {
                return FailedDependency(entityValidationException.InnerException);
            }
            catch (EntityDependencyValidationException entityDependencyValidationException)
               when (entityDependencyValidationException.InnerException is AlreadyExistsEntityException)
            {
                return Conflict(entityDependencyValidationException.InnerException);
            }
            catch (EntityDependencyException entityDependencyException)
            {
                return InternalServerError(entityDependencyException);
            }
            catch (EntityServiceException standardServiceException)
            {
                return InternalServerError(standardServiceException);
            }
        }

        [HttpDelete("{itemId}")]
        public virtual async ValueTask<ActionResult<TEntity>> DeleteEntityByIdAsync(Guid itemId)
        {
            try
            {
                TEntity deletedEntity =
                    await this.standardService.RemoveEntityByIdAsync(itemId);

                return Ok(deletedEntity);
            }
            catch (EntityValidationException entityValidationException)
                when (entityValidationException.InnerException is NotFoundEntityException)
            {
                return NotFound(entityValidationException.InnerException);
            }
            catch (EntityValidationException entityValidationException)
            {
                return BadRequest(entityValidationException.InnerException);
            }
            catch (EntityDependencyValidationException entityDependencyValidationException)
                when (entityDependencyValidationException.InnerException is LockedEntityException)
            {
                return Locked(entityDependencyValidationException.InnerException);
            }
            catch (EntityDependencyValidationException entityDependencyValidationException)
            {
                return BadRequest(entityDependencyValidationException);
            }
            catch (EntityDependencyException entityDependencyException)
            {
                return InternalServerError(entityDependencyException);
            }
            catch (EntityServiceException standardServiceException)
            {
                return InternalServerError(standardServiceException);
            }
        }
    }
}