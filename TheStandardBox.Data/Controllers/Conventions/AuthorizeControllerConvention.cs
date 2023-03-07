using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Reflection;
using TheStandardBox.Core.Attributes.Contollers;

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

                if (authorizeAttribute != null && customControllerAttribute.AnonymousActions == null)
                {
                    AuthorizeFilter authorizeFilter = new AuthorizeFilter(authorizeAttribute.Policy);
                    controller.Filters.Add(authorizeFilter);
                }
            }
        }
    }
}
