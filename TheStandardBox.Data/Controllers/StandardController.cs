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
        private readonly IStandardService<TEntity> standardService;

        public StandardController(IStandardService<TEntity> standardService) =>
            this.standardService = standardService;

        [HttpPost]
        public virtual async ValueTask<ActionResult<TEntity>> PostEntityAsync(TEntity model)
        {
            try
            {
                TEntity addedEntity =
                    await this.standardService.AddEntityAsync(model);

                return Created(addedEntity);
            }
            catch (EntityValidationException modelValidationException)
            {
                return BadRequest(modelValidationException.InnerException);
            }
            catch (EntityDependencyValidationException modelValidationException)
                when (modelValidationException.InnerException is InvalidEntityReferenceException)
            {
                return FailedDependency(modelValidationException.InnerException);
            }
            catch (EntityDependencyValidationException modelDependencyValidationException)
               when (modelDependencyValidationException.InnerException is AlreadyExistsEntityException)
            {
                return Conflict(modelDependencyValidationException.InnerException);
            }
            catch (EntityDependencyException modelDependencyException)
            {
                return InternalServerError(modelDependencyException);
            }
            catch (EntityServiceException standardServiceException)
            {
                return InternalServerError(standardServiceException);
            }
        }

        [HttpGet]
        public virtual ActionResult<IQueryable<TEntity>> GetAllEntitys()
        {
            try
            {
                IQueryable<TEntity> retrievedEntitys =
                    this.standardService.RetrieveAllEntities();

                return Ok(retrievedEntitys);
            }
            catch (EntityDependencyException modelDependencyException)
            {
                return InternalServerError(modelDependencyException);
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
                TEntity model = await this.standardService.RetrieveEntityByIdAsync(itemId);

                return Ok(model);
            }
            catch (EntityValidationException modelValidationException)
                when (modelValidationException.InnerException is NotFoundEntityException)
            {
                return NotFound(modelValidationException.InnerException);
            }
            catch (EntityValidationException modelValidationException)
            {
                return BadRequest(modelValidationException.InnerException);
            }
            catch (EntityDependencyException modelDependencyException)
            {
                return InternalServerError(modelDependencyException);
            }
            catch (EntityServiceException standardServiceException)
            {
                return InternalServerError(standardServiceException);
            }
        }

        [HttpPut]
        public virtual async ValueTask<ActionResult<TEntity>> PutEntityAsync(TEntity model)
        {
            try
            {
                TEntity modifiedEntity =
                    await this.standardService.ModifyEntityAsync(model);

                return Ok(modifiedEntity);
            }
            catch (EntityValidationException modelValidationException)
                when (modelValidationException.InnerException is NotFoundEntityException)
            {
                return NotFound(modelValidationException.InnerException);
            }
            catch (EntityValidationException modelValidationException)
            {
                return BadRequest(modelValidationException.InnerException);
            }
            catch (EntityDependencyValidationException modelValidationException)
                when (modelValidationException.InnerException is InvalidEntityReferenceException)
            {
                return FailedDependency(modelValidationException.InnerException);
            }
            catch (EntityDependencyValidationException modelDependencyValidationException)
               when (modelDependencyValidationException.InnerException is AlreadyExistsEntityException)
            {
                return Conflict(modelDependencyValidationException.InnerException);
            }
            catch (EntityDependencyException modelDependencyException)
            {
                return InternalServerError(modelDependencyException);
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
            catch (EntityValidationException modelValidationException)
                when (modelValidationException.InnerException is NotFoundEntityException)
            {
                return NotFound(modelValidationException.InnerException);
            }
            catch (EntityValidationException modelValidationException)
            {
                return BadRequest(modelValidationException.InnerException);
            }
            catch (EntityDependencyValidationException modelDependencyValidationException)
                when (modelDependencyValidationException.InnerException is LockedEntityException)
            {
                return Locked(modelDependencyValidationException.InnerException);
            }
            catch (EntityDependencyValidationException modelDependencyValidationException)
            {
                return BadRequest(modelDependencyValidationException);
            }
            catch (EntityDependencyException modelDependencyException)
            {
                return InternalServerError(modelDependencyException);
            }
            catch (EntityServiceException standardServiceException)
            {
                return InternalServerError(standardServiceException);
            }
        }
    }
}