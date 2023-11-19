
namespace ApiTAF.Utils
{
    public static class ReadJsonShemaFromFile
    {
        public static async Task<string> ReadJsonSchemaFromFileAsync(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
