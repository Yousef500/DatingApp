namespace API.Helpers
{
  public class LogUserActivity : IAsyncActionFilter
  {
    public async Task OnActionExecutionAsync(ActionExecutionContext context, AcionExecutionDelegate next)
    {
      var resultContext = await next();

      if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

      var userId = resultContext.HttpContext.User.GetUserId();
      var repo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();

      var user = await repo.GetUserById(userId);

      user.LastActive = DateTime.Now;

      await repo.SaveAllAsync();
    }
  }
}