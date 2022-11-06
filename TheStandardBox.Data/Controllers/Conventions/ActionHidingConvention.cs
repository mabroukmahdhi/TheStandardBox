// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using TheStandardBox.Core.Extensions;
using TheStandardBox.Core.Models.Foundations.Joins;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace TheStandardBox.Data.Controllers.Conventions
{
    public class ActionHidingConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            if (action.ActionName == "GetEntityByIds")
            {
                action.ApiExplorer.IsVisible =
                    IsVisibleAction(action, nameof(IJoinEntity));
                return;
            }

            if (action.ActionName == "GetEntityById")
            {
                action.ApiExplorer.IsVisible =
                    IsVisibleAction(action, nameof(IStandardEntity));
            }
        }

        private bool IsVisibleAction(ActionModel action, string interfaceName)
        {
            var controller = action.Controller;
            if (controller.ControllerProperties.Any(p => p.PropertyName == "Entity"))
            {
                var entityType = controller.ControllerProperties.FirstOrDefault(p =>
                    p.PropertyName == "Entity");

                return entityType.PropertyInfo.PropertyType.ImplementsInterface(interfaceName);
            }

            return false;
        }
    }
}
