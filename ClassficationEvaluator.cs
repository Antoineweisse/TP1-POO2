public struct EvaluationResult
{
    public double Accuracy { get; set; }
    public int[,] ConfusionMatrix { get; set; }
}

public class ClassificationEvaluator
{
    public EvaluationResult Evaluate(IKnnClassifier classifier, List<Seed> testData)
    {
        if (testData == null || testData.Count == 0)
        {
            throw new ArgumentException("Le jeu de données de test ne peut pas être null ou vide.");
        }
        var labels = new Dictionary<string, int>{
            { "Kama", 0 },
            { "Rosa", 1 },
            { "Canadian", 2 }
        };
        int correct = 0;
        int[,] confusionMatrix = new int[3, 3];

        foreach (var seed in testData)
        {
            string predicted = classifier.Predict(seed);
            string actual = seed.Variety ?? string.Empty;

            int actualIndex = labels[actual];
            int predictedIndex = labels[predicted];

            confusionMatrix[actualIndex, predictedIndex]++;

            if (predicted == seed.Variety)
                correct++;
        }

        return new EvaluationResult
        {
            Accuracy = (double)correct / testData.Count,
            ConfusionMatrix = confusionMatrix
        };
    }
}