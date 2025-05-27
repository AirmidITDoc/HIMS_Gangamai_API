using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HIMS.API.Comman
{
        public class ValidateModelAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                if (!context.ModelState.IsValid)
                {
                    var errors = context.ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    var response = ApiResponseHelper.GenerateResponse<string>(
                       ApiStatusCode.Status400BadRequest,
                       "Validation failed.",
                       null
                   );

                    context.Result = new BadRequestObjectResult(response);
                }
            }
        }
}
