using Newtonsoft.Json;

public class JsonManager
{
    public void WriteJson(string filePath, object data,Formatting formatting = Formatting.Indented)
    {
        var json = JsonConvert.SerializeObject(data, formatting);
        File.WriteAllText(filePath, json);
    }
} 