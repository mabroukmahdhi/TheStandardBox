using System.Drawing;
using System;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using TheStandardBox.Core.Attributes.Contollers;
using TheStandardBox.Core.Models.Controllers;
using Microsoft.AspNetCore.Mvc;
using Action = TheStandardBox.Core.Models.Controllers.Action;

namespace TheStandardBox.Data.Controllers.Conventions
{
    public class AuthorizeControllerConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType)
            {
                var genericType = controller.ControllerType.GenericTypeArguments[0];
                var authorizeAttribute = genericType.GetCustomAttribute<AuthorizeAttribute>();
                var customControllerAttribute = genericType.GetCustomAttribute<GeneratedControllerAttribute>();

                if (authorizeAttribute == null) return;

                // TODO: Simplify If conditions

                if (customControllerAttribute.AnonymousMethods.HasFlag(Action.GetEntityById) 
                    && controller.ControllerName == "GetEntityById") 
                {
                    return;
                }

                if (customControllerAttribute.AnonymousMethods.HasFlag(Action.PutEntity)
                    && controller.ControllerName == "PutEntity")
                {
                    return;
                }

                if (customControllerAttribute.AnonymousMethods.HasFlag(Action.GetAllEntities)
                    && controller.ControllerName == "GetAllEntities")
                {
                    return;
                }

                if (customControllerAttribute.AnonymousMethods.HasFlag(Action.PostEntity)
                    && controller.ControllerName == "PostEntity")
                {
                    return;
                }

                if (customControllerAttribute.AnonymousMethods.HasFlag(Action.DeleteEntityById)
                    && controller.ControllerName == "DeleteEntityById")
                {
                    return;
                }

                AuthorizeFilter authorizeFilter = new AuthorizeFilter(authorizeAttribute.Policy);
                controller.Filters.Add(authorizeFilter);
            }
        }
    }
}
