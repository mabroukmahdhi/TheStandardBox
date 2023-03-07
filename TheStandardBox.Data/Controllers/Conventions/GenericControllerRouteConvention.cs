// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;
using System.Reflection;
using TheStandardBox.Core.Attributes.Contollers;

namespace TheStandardBox.Data.Controllers.Conventions
{
    public class GenericControllerRouteConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType)
            {
                var genericType = controller.ControllerType.GenericTypeArguments[0];
                var customNameAttribute = genericType.GetCustomAttribute<GeneratedControllerAttribute>();
                if (customNameAttribute?.Route != null)
                {
                    var name = customNameAttribute.Route.Split('/').LastOrDefault();
                    controller.ControllerName = $"{name}GenericController";
                    controller.Selectors.Add(new SelectorModel
                    {
                        AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(customNameAttribute.Route)),

                    });
                }
                else
                {
                    controller.ControllerName = genericType.Name;
                }
            }
        }
    }
}
