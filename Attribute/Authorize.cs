using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthorizeRoleAttribute : Attribute, IAuthorizationFilter
{
  private readonly string[] _roles;

  public AuthorizeRoleAttribute(params string[] roles)
  {
    _roles = roles;
  }

  public void OnAuthorization(AuthorizationFilterContext context)
  {
    var session = context.HttpContext.Session;
    var userId = session.GetString("UserID");
    var userRole = session.GetString("UserRole");

    if (string.IsNullOrEmpty(userId) || !_roles.Contains(userRole))
    {
      context.Result = new RedirectToActionResult("Login", "Auth", null);
    }
  }
}
