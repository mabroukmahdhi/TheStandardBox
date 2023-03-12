using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using TheStandardBox.Core.Attributes.Contollers;

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

                    if (!customNameAttribute.AnonymousActions?.Any(a => a.ToString() == action.ActionName) ?? false)
                    {
                        Console.WriteLine(action.ActionName);
                        action.Filters.Add(new AuthorizeFilter(authorizeAttribute.Policy));
                    }

                }
            }
            catch (Exception e) { }
        }
    }
}
