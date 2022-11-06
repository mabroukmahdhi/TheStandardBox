// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using TheStandardBox.Core.Models.Controllers;

namespace TheStandardBox.Core.Attributes.Contollers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GeneratedControllerAttribute : Attribute
    {
        public GeneratedControllerAttribute(
            string route,
            params AllowedAction[] allowedActions)
        {
            Route = route;

            if (allowedActions?.Any() == true)
            {
                AllowedActions = allowedActions;
            }
            else if (allowedActions.Contains(AllowedAction.None))
            {
                AllowedActions = Array.Empty<AllowedAction>();
            }
            else
            {
                var list = new List<AllowedAction>
                {
                     AllowedAction.GetAllEntities,
                     AllowedAction.GetEntityById,
                     AllowedAction.PostEntity,
                     AllowedAction.PutEntity,
                     AllowedAction.DeleteEntityById
                };

                AllowedActions = list.ToArray();
            }
        }

        public string Route { get; }
        public AllowedAction[] AllowedActions { get; }
    }
}
