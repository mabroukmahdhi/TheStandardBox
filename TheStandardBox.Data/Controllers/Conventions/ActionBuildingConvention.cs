﻿// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using TheStandardBox.Core.Attributes.Contollers;
using TheStandardBox.Core.Extensions;
using TheStandardBox.Core.Models.Controllers;
using TheStandardBox.Core.Models.Foundations.Joins;
using TheStandardBox.Core.Models.Foundations.Standards;
using Action = TheStandardBox.Core.Models.Controllers.Action;

namespace TheStandardBox.Data.Controllers.Conventions
{
    public class ActionBuildingConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            try
            {
                if (action.Controller.ControllerName.EndsWith("GenericController"))
                {
                    var genericType = action.Controller.ControllerType.GenericTypeArguments[0];
                    var customNameAttribute = genericType.GetCustomAttribute<GeneratedControllerAttribute>();

                    Enum.TryParse(action.ActionName, out Action actionValue);
                    action.ApiExplorer.IsVisible = !customNameAttribute.DisabledMethods.HasFlag(actionValue);

                    var allowsGetById = !customNameAttribute.DisabledMethods.HasFlag(Action.GetEntityById);

                    var allowsDeleteById = !customNameAttribute.DisabledMethods.HasFlag(Action.DeleteEntityById);

                    if ((action.ActionName == "GetEntityByIds" && allowsGetById)
                        || (action.ActionName == "DeleteEntityByIds" && allowsDeleteById))
                    {
                        if (IsVisibleAction(action, nameof(IJoinEntity)))
                        {
                            action.ApiExplorer.IsVisible = true;

                            var id = genericType.GetPrimaryKeyName();
                            var ids = id?.Split('_')?.Select(i => i.Replace("Id", "s"))?.ToList();
                            if (ids != null)
                            {
                                var routeModel = action.Selectors.FirstOrDefault().AttributeRouteModel;

                                routeModel.Template = $"{ids[0]?.ToLower()}/{{entityId1}}/{ids[1]?.ToLower()}/{{entityId2}}";
                            }
                        }
                        return;
                    }

                    if (action.ActionName == "GetEntityById"
                        && allowsGetById)
                    {
                        action.ApiExplorer.IsVisible =
                            IsVisibleAction(action, nameof(IStandardEntity));
                    }

                    if (action.ActionName == "DeleteEntityById"
                        && allowsDeleteById)
                    {
                        action.ApiExplorer.IsVisible =
                            IsVisibleAction(action, nameof(IStandardEntity));
                    }
                }
            }
            catch { }
        }

        private bool IsVisibleAction(ActionModel action, string interfaceName)
        {
            try
            {
                if (action.Controller.ControllerType.IsGenericType)
                {
                    var genericType = action.Controller.ControllerType.GenericTypeArguments[0];
                    return genericType.ImplementsInterface(interfaceName);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
