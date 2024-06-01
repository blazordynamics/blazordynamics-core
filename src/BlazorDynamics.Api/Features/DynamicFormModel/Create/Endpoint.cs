using BlazorFormFactory.Contracts;
using BlazorFormFactory.DynamicUI.JsonSchema.Implementations;
using BlazorFormFactory.UISchema.Implementations;
using FastEndpoints;
using Newtonsoft.Json.Linq;

namespace BlazorFormFactory.Api.Features.DynamicFormModel.Create
{
    public class Endpoint : Endpoint<Request, Response>
    {
        public override void Configure()
        {
            Post("/api/dynamic-form-model/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {

            var test = JObject.Parse(req.JsonSchema.ToString());
            var uiSchema = JToken.Parse(req.UISchema.ToString());


            var sut = new DynamicFormModelCreator(
               new UISchemaParser(new JsonSchemaScopeProvider(new SchemaReader(), test)));

            var models = sut.GenerateModels(uiSchema);
            await SendAsync(new Response
            {
                Models = models.ToList()
            });
        }
    }

    public class Request
    {
        public object UISchema { get; set; }

        public object JsonSchema { get; set; }
    }

    public class Response
    {
        public List<Models.DynamicFormModel> Models { get; set; }
    }
}
