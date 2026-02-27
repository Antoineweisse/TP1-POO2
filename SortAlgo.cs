public interface ISortAlgo
{
    List<(Seed seed, double distance)> Sort(List<(Seed seed, double distance)> distances);
}


public class BubbleSort : ISortAlgo
{
    public List<(Seed seed, double distance)> Sort(List<(Seed seed, double distance)> distances)
    {
        int n = distances.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (distances[j].distance > distances[j + 1].distance)
                {
                    var temp = distances[j];
                    distances[j] = distances[j + 1];
                    distances[j + 1] = temp;
                }
            }
        }
        return distances;
    }
}