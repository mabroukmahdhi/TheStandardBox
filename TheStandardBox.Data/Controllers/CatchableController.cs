// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using TheStandardBox.Core.Models.Foundations.Bases.Exceptions;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace TheStandardBox.Data.Controllers
{
    public class CatchableController<TEntity> : RESTFulController
        where TEntity : class, IStandardEntity
    {
        protected delegate ValueTask<TEntity> ReturningEntityFunction();
        protected delegate IQueryable<TEntity> ReturningEntitiesFunction();

        protected virtual async ValueTask<ActionResult<TEntity>> TryCatchOnPost(ReturningEntityFunction returningEntityFunction)
        {
            try
            {
                TEntity addedEntity = await returningEntityFunction();

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

        protected virtual async ValueTask<ActionResult<TEntity>> TryCatchOnGetById(ReturningEntityFunction returningEntityFunction)
        {
            try
            {
                TEntity entity = await returningEntityFunction();

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

        protected virtual ActionResult<IQueryable<TEntity>> TryCatchOnGetAll(ReturningEntitiesFunction returningEntitiesFunction)
        {
            try
            {
                var retrievedEntities = returningEntitiesFunction();

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

        protected virtual async ValueTask<ActionResult<TEntity>> TryCatchOnPut(ReturningEntityFunction returningEntityFunction)
        {
            try
            {
                TEntity modifiedEntity = await returningEntityFunction();

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

        protected virtual async ValueTask<ActionResult<TEntity>> TryCatchOnDelete(ReturningEntityFunction returningEntityFunction)
        {
            try
            {
                TEntity deletedEntity = await returningEntityFunction();

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