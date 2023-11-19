using NJsonSchema;
using NUnit.Framework;
using RestSharp;

namespace ApiTAF.Utils
{
    public static class CheckJsonSchema
    {
        public static async Task CheckShemaAsync(RestResponse response, string schema)
        {
            JsonSchema s = await JsonSchema.FromJsonAsync(schema);
            var validationResult = s.Validate(response.Content);
            Assert.That(validationResult.Count, Is.EqualTo(10), "Response body is not the array of 10 users");
        }
    }
}
