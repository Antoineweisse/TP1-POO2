using CsvHelper;

public class CsvManager
{

    public List<Dictionary<string, object>> ReadCsv(string filePath)
    {
        var config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            DetectDelimiter = true
        };

        List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            var records = csv.GetRecords<dynamic>().ToList();
            foreach (var record in records)
            {
                var dictionary = (IDictionary<string, object>)record;
                results.Add(new Dictionary<string, object>(dictionary));
            }
        }
        return results;
    }
    
}