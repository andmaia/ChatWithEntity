using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Application
{
    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Adicione a verificação de autorização aqui
            var hasAuthorization = context.HttpContext.User.Identity.IsAuthenticated;

            if (!hasAuthorization)
            {
                context.Result = new ObjectResult("Por favor, forneça um token de autenticação.")
                {
                    StatusCode = 401
                };
            }
        }
    }
}