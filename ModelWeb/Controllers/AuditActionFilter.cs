
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Data.Models;
using Services.Data.Repository;
using System.Text.Json;
public class AuditActionFilter : IAsyncActionFilter
{
	private readonly IRepository<AuditLog> _context;
	
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditActionFilter(IRepository<AuditLog> context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
	public partial class AuditLog
	{
		public int AuditLogId { get; set; }

		public string TableName { get; set; }

		public string PrimaryKeyValue { get; set; }

		public string Action { get; set; }

		public string ControllerName { get; set; }

		public string UserName { get; set; }

		public DateTime? Timestamp { get; set; }
	}
	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (resultContext.Exception == null)
        {
            var controllerName = context.ActionDescriptor.RouteValues["controller"];
            var actionName = context.ActionDescriptor.RouteValues["action"];
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";

            var routeData = context.RouteData.Values;
            var id = routeData.ContainsKey("id") ? routeData["id"]?.ToString() : null;
            var parametersJson = JsonSerializer.Serialize(context.ActionArguments);
            var auditLog = new AuditLog
            {
                TableName = controllerName,
                PrimaryKeyValue = parametersJson,
                Action = actionName,
                ControllerName = controllerName,
                UserName = userName,
                Timestamp = DateTime.UtcNow
            };

          await  _context.AddAsync(auditLog);
           
        }
    }
}
