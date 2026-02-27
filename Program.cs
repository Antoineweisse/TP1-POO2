using Spectre.Console;

class Programm
{
    static void Main(string[] args)
    {
        int k = AnsiConsole.Ask<int>("Enter the value of [yellow]k[/]:");
        string distanceChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a [green]distance algorithm[/]:")
                .AddChoices(new[] { "Euclidean", "Manhattan" })
        );
        string sortChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a [green]sorting algorithm[/]:")
                .AddChoices(new[] { "Bubble Sort" })
        );

        CsvManager csvManager = new CsvManager();
        SeedFactory seedFactory = new SeedFactory();

        IDistance distanceAlgo = distanceChoice switch
        {
            "Euclidean" => new EuclideanDistance(),
            "Manhattan" => new ManhattanDistance(),
            _ => throw new InvalidOperationException("Invalid distance algorithm choice")
        };

        ISortAlgo sortAlgo = sortChoice switch
        {
            "Bubble Sort" => new BubbleSort(),
            _ => throw new InvalidOperationException("Invalid sorting algorithm choice")
        };

        string? dataSetTrainPath = null;
        while (string.IsNullOrEmpty(dataSetTrainPath) || !File.Exists(dataSetTrainPath))
        {
            dataSetTrainPath = AnsiConsole.Ask<string>("Enter the path for the [yellow]training dataset[/]:");
            if (!File.Exists(dataSetTrainPath))
            {
                AnsiConsole.MarkupLine("[red]File not found. Please enter a valid path.[/]");
            }
        }
        string? dataSetTestPath = null;
        while (string.IsNullOrEmpty(dataSetTestPath) || !File.Exists(dataSetTestPath))
        {
            dataSetTestPath = AnsiConsole.Ask<string>("Enter the path for the [yellow]testing dataset[/]:");
            if (!File.Exists(dataSetTestPath))
            {
                AnsiConsole.MarkupLine("[red]File not found. Please enter a valid path.[/]");
            }
        }
        string outputPath = AnsiConsole.Ask<string>("Enter the path for the [yellow]output results[/]:");


        ClassificationEvaluator evaluator = new ClassificationEvaluator();
        JsonManager jsonManager = new JsonManager();
        
        var dictTrainSeeds = csvManager.ReadCsv(dataSetTrainPath);
        var dictTestSeeds = csvManager.ReadCsv(dataSetTestPath);

        var trainSeeds = seedFactory.CreateSeeds(dictTrainSeeds);
        var testSeeds = seedFactory.CreateSeeds(dictTestSeeds);

        var knn = new KnnClassifier(trainSeeds, k, distanceAlgo, sortAlgo);
        var result = evaluator.Evaluate(knn, testSeeds);

        jsonManager.WriteJson(outputPath, new {
            Execution = new {
                distanceAlgorithme = distanceAlgo.GetType().Name,
                sortAlgorithme = sortAlgo.GetType().Name,
                K = k,
                Timestamp = DateTime.Now,
            },
            Dataset = new {
                TrainingSize = dictTrainSeeds.Count,
                TestSize = dictTestSeeds.Count
            },
            Metrics = new {
                Accuracy = Math.Round(result.Accuracy, 4) * 100,
                result.ConfusionMatrix
            }
        });
        AnsiConsole.MarkupLine($"[green]Evaluation completed! Results saved to {outputPath}[/]");
    }
}