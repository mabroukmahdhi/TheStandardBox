// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using TheStandardBox.Core.Attributes.Contollers;
using TheStandardBox.Core.Extensions;
using TheStandardBox.Core.Models.Controllers;
using TheStandardBox.Core.Models.Foundations.Joins;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace TheStandardBox.Data.Controllers.Conventions
{
    public class ActionHidingConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            if (action.Controller.ControllerName.EndsWith("GenericController"))
            {
                var genericType = action.Controller.ControllerType.GenericTypeArguments[0];
                var customNameAttribute = genericType.GetCustomAttribute<GeneratedControllerAttribute>();

                action.ApiExplorer.IsVisible =
                    customNameAttribute.AllowedActions?.Any(a =>
                        a.ToString() == action.ActionName) ?? false;

                var allowsGetById = customNameAttribute.AllowedActions.Contains(
                    AllowedAction.GetEntityById);

                if (action.ActionName == "GetEntityByIds"
                    && allowsGetById)
                {
                    action.ApiExplorer.IsVisible =
                        IsVisibleAction(action, nameof(IJoinEntity));
                    return;
                }

                if (action.ActionName == "GetEntityById"
                    && allowsGetById)
                {
                    action.ApiExplorer.IsVisible =
                        IsVisibleAction(action, nameof(IStandardEntity));
                }
            }
        }

        private bool IsVisibleAction(ActionModel action, string interfaceName)
        {
            if (action.Controller.ControllerType.IsGenericType)
            {
                var genericType = action.Controller.ControllerType.GenericTypeArguments[0];
                return genericType.ImplementsInterface(interfaceName);
            }
            return false;
        }
    }
}
