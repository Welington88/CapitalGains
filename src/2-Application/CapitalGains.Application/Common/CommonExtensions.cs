using Newtonsoft.Json;

namespace CapitalGains.Application.Common
{
    public static class JsonHelperExtensions
    {
        
        public static List<T> DeserializeJsonToList<T>(string json)
            => JsonConvert.DeserializeObject<List<T>>(json)!;

        public static IEnumerable<string> GetJsonLines(string inputData)
        {
            var lines = inputData
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim())
                .Where(line => line.StartsWith('[') && line.EndsWith(']'))
                .ToList();

            if (lines.Count == 0)
                lines.Add(inputData);

            return lines;
        }

    }
}