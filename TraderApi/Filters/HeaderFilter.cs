using Microsoft.Build.Evaluation;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TraderApi.Filters
{
    
    /// <summary>
    /// Operation filter to add the requirement of the custom header
    /// </summary>
    public class HeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "TraderApiKey",
                In = ParameterLocation.Header,                
                Required = true 
            });
        }
       
    }
}
