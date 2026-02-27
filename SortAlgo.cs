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

public class MergeSort : ISortAlgo
{
    public List<(Seed seed, double distance)> Sort(List<(Seed seed, double distance)> distances)
    {
        if (distances.Count <= 1)
            return distances;

        int mid = distances.Count / 2;
        var left = Sort(distances.GetRange(0, mid));
        var right = Sort(distances.GetRange(mid, distances.Count - mid));

        return Merge(left, right);
    }

    private List<(Seed seed, double distance)> Merge(List<(Seed seed, double distance)> left, List<(Seed seed, double distance)> right)
    {
        List<(Seed seed, double distance)> result = new List<(Seed seed, double distance)>();
        int i = 0, j = 0;

        while (i < left.Count && j < right.Count)
        {
            if (left[i].distance <= right[j].distance)
            {
                result.Add(left[i]);
                i++;
            }
            else
            {
                result.Add(right[j]);
                j++;
            }
        }

        while (i < left.Count)
        {
            result.Add(left[i]);
            i++;
        }

        while (j < right.Count)
        {
            result.Add(right[j]);
            j++;
        }

        return result;
    }
}