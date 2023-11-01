using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Net7.WebApi.Template.Enums;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Net7.WebApi.Template.Helpers
{
    public class EnumerationToEnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (!context.Type.IsSubclassOf(typeof(EnumerationBase)))
            {
                return;
            }

            var fields = context.Type.GetFields(BindingFlags.Static | BindingFlags.Public);

            schema.Enum = fields.Select(field => new OpenApiString(field.Name)).Cast<IOpenApiAny>().ToList();
            schema.Enum.Insert(0, null);
            schema.Type = "string";
            schema.Properties = null;
            schema.AllOf = null;
        }
    }
}