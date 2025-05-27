public class AuthenticationMiddleware
{
  private readonly RequestDelegate _next;

  public AuthenticationMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    var path = context.Request.Path;
    var session = context.Session;
    var userId = session.GetString("UserID");

    // Các route yêu cầu đăng nhập
    string[] protectedRoutes = {
            "/get-ticket/passenger-info",
            "/payment",
            "/booking-confirm"
        };

    if (protectedRoutes.Any(route => path.StartsWithSegments(route)) && string.IsNullOrEmpty(userId))
    {
      // Lưu URL hiện tại vào TempData để redirect sau khi đăng nhập
      context.Session.SetString("ReturnUrl", path);
      context.Response.Redirect("/login");
      return;
    }

    await _next(context);
  }
}