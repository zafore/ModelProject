using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuditActionFilterAttribute : TypeFilterAttribute
{
    public AuditActionFilterAttribute() : base(typeof(AuditActionFilter))
    {
    }
}
