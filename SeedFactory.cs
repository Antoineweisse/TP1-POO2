public class SeedFactory
{
    public SeedFactory()
    {
        // Constructor logic here
    }

    private bool checkData(IDictionary<string, object> data)
    {
        // Implement any necessary validation logic here
        return data.ContainsKey("variety") && 
               data.ContainsKey("Area") && 
               data.ContainsKey("Perimeter") && 
               data.ContainsKey("Compactness") && 
               data.ContainsKey("Kernel_Length") && 
               data.ContainsKey("Kernel_Width") && 
               data.ContainsKey("Asymmetry_Coefficient") && 
               data.ContainsKey("Groove_Length");
    }

    public Seed CreateSeed(IDictionary<string, object> data)
    {
        if (!checkData(data))
        {
            throw new ArgumentException("Invalid seed data");
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