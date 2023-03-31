using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using TheStandardBox.Core.Attributes.Contollers;
using Action = TheStandardBox.Core.Models.Controllers.Action;

namespace TheStandardBox.Data.Controllers.Conventions
{
    public class ActionAuthorizingConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            try
            {
                if (action.Controller.ControllerName.EndsWith("GenericController"))
                {
                    var genericType = action.Controller.ControllerType.GenericTypeArguments[0];
                    var authorizeAttribute = genericType.GetCustomAttribute<AuthorizeAttribute>();
                    var customNameAttribute = genericType.GetCustomAttribute<GeneratedControllerAttribute>();

                    if (authorizeAttribute == null)
                        return;

                    /*if (customNameAttribute.AnonymousMethods.HasFlag(Action.None))
                        return;*/

                    /*AuthorizeFilter authorizeFilter = new AuthorizeFilter(authorizeAttribute.Policy);
                    action.Filters.Add(authorizeFilter);*/
                }
            }
            catch (Exception e) { }
        }
    }
}
