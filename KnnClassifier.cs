public interface IKnnClassifier
{
   string Predict(Seed sample);
}


public class KnnClassifier : IKnnClassifier
{
    private List<Seed> _trainingData;
    private int _k;
    private IDistance _distanceAlgo;
    private ISortAlgo _sortAlgo;

    public KnnClassifier(List<Seed> trainingData, int k, IDistance distanceAlgo, ISortAlgo sortAlgo)
    {
        if (trainingData == null || trainingData.Count == 0)
            throw new ArgumentException("Le dataset d'entraînement ne peut pas être vide");
        if (k <= 0)
            throw new ArgumentException("K doit être un entier positif");
        if (k > trainingData.Count)
            throw new ArgumentException("K ne peut pas être supérieur à la taille du dataset d'entraînement");
        if (distanceAlgo == null)
            throw new ArgumentNullException(nameof(distanceAlgo));
        if (sortAlgo == null)
            throw new ArgumentNullException(nameof(sortAlgo));
        
        _trainingData = trainingData;
        _k = k;
        _distanceAlgo = distanceAlgo;
        _sortAlgo = sortAlgo;
    }

    public string Predict(Seed sample)
    {
        if (sample == null)
            throw new ArgumentNullException(nameof(sample));
        
        var distances = new List<(Seed seed, double distance)>();   
        foreach (var seed in _trainingData)
        {
            double distance = _distanceAlgo.CalculateDistance(sample, seed);
            distances.Add((seed, distance));
        }

        distances = _sortAlgo.Sort(distances);
        var nearest = distances.Take(_k).ToList();
    
        return MajorityVote(nearest);
    }

    private string MajorityVote(List<(Seed seed, double distance)> neighbors)
    {
        var grouped = neighbors
        .GroupBy(n => n.seed.Variety)
        .Select(g => new
        {
            Variety = g.Key,
            Count = g.Count(),
            MinDistance = g.Min(x => x.distance)
        })
        .ToList();
        
        int maxVotes = grouped.Max(g => g.Count);
        var topGroups = grouped
            .Where(g => g.Count == maxVotes)
            .ToList();
        if (topGroups.Count == 1)
            return topGroups.First().Variety;
        return topGroups.OrderBy(g => g.MinDistance).First().Variety;
    }
}