// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace TheStandardBox.Data.Controllers.Conventions
{
    public class ActionHidingConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            // Replace with any logic you want
            if (action.Controller.ControllerName == "Second")
            {
                action.ApiExplorer.IsVisible = false;
            }
        }
    }
}
