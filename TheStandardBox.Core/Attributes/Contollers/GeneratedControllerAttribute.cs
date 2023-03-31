// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using TheStandardBox.Core.Models.Controllers;
using Action = TheStandardBox.Core.Models.Controllers.Action;

namespace TheStandardBox.Core.Attributes.Contollers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GeneratedControllerAttribute : Attribute
    {
        public GeneratedControllerAttribute(
            string route,
            Action disabledMethods = Action.None,
            Action anonymousMethods = Action.None)
        {
            Route = route;
            DisabledMethods = disabledMethods;
            AnonymousMethods = anonymousMethods;

        }

        public string Route { get; set; }
        public Action DisabledMethods { get; set; } = Action.None;
        public Action AnonymousMethods { get; set; } = Action.None;
    }
}
