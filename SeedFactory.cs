public class SeedFactory
{
    private bool checkData(IDictionary<string, object> data)
    {
        bool hasAllKeys = data.ContainsKey("variety") && 
                          data.ContainsKey("Area") && 
                          data.ContainsKey("Perimeter") && 
                          data.ContainsKey("Compactness") && 
                          data.ContainsKey("Kernel_Length") && 
                          data.ContainsKey("Kernel_Width") && 
                          data.ContainsKey("Asymmetry_Coefficient") && 
                          data.ContainsKey("Groove_Length");
        if (!hasAllKeys)
            return false;
        
        bool hasNullValues = string.IsNullOrWhiteSpace(data["variety"].ToString()) || 
                             string.IsNullOrWhiteSpace(data["Area"].ToString()) ||
                             string.IsNullOrWhiteSpace(data["Perimeter"].ToString()) ||
                             string.IsNullOrWhiteSpace(data["Compactness"].ToString()) || 
                             string.IsNullOrWhiteSpace(data["Kernel_Length"].ToString()) ||
                             string.IsNullOrWhiteSpace(data["Kernel_Width"].ToString()) ||
                             string.IsNullOrWhiteSpace(data["Asymmetry_Coefficient"].ToString()) ||
                             string.IsNullOrWhiteSpace(data["Groove_Length"].ToString());
        if (hasNullValues)
            return false;
        
        if (data["variety"].ToString() != "Kama" && 
            data["variety"].ToString() != "Rosa" && 
            data["variety"].ToString() != "Canadian")
        {
            return false;
        }
        
        bool isNotConvertible = !double.TryParse(data["Area"].ToString(), out _) ||
                             !double.TryParse(data["Perimeter"].ToString(), out _) ||
                             !double.TryParse(data["Compactness"].ToString(), out _) ||
                             !double.TryParse(data["Kernel_Length"].ToString(), out _) ||
                             !double.TryParse(data["Kernel_Width"].ToString(), out _) ||
                             !double.TryParse(data["Asymmetry_Coefficient"].ToString(), out _) ||
                             !double.TryParse(data["Groove_Length"].ToString(), out _);
        if (isNotConvertible)
            return false;
        
        return true;
    }

    public Seed CreateSeed(IDictionary<string, object> data)
    {
        if (!checkData(data))
        {
            throw new ArgumentException("Données invalides pour la création d'une graine.");
        }
        return new Seed(
            data["variety"].ToString() ?? string.Empty,
            Convert.ToDouble(data["Area"]),
            Convert.ToDouble(data["Perimeter"]),
            Convert.ToDouble(data["Compactness"]),
            Convert.ToDouble(data["Kernel_Length"]),
            Convert.ToDouble(data["Kernel_Width"]),
            Convert.ToDouble(data["Asymmetry_Coefficient"]),
            Convert.ToDouble(data["Groove_Length"])
        );
    }

    public List<Seed> CreateSeeds(List<Dictionary<string, object>> dataList)
    {
        List<Seed> seeds = new List<Seed>();
        foreach (var data in dataList)
        {
            seeds.Add(CreateSeed(data));
        }
        return seeds;
    }
}