using Spectre.Console;
class KnnApplication
{
    public void Run()
    {
        int k = AnsiConsole.Ask<int>("Entrer la valeur de [yellow]k[/]:");
        IDistance distanceAlgo = AskDistanceAlgorithm();
        ISortAlgo sortAlgo = AskSortingAlgorithm();
        string dataSetTrainPath = AskFilePath("jeu de données d'entraînement");
        string dataSetTestPath = AskFilePath("jeu de données de test");
        string outputPath = AnsiConsole.Ask<string>("Entrer le chemin pour les [yellow]résultats de sortie[/]:");
    
        ProcessKnn(k, distanceAlgo, sortAlgo, dataSetTrainPath, dataSetTestPath, outputPath);
    }

    private IDistance AskDistanceAlgorithm()
    {
        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choisissez un [green]algorithme de distance[/]:")
                .AddChoices("Euclidean", "Manhattan")
        );

        return choice switch
        {
            "Euclidean" => new EuclideanDistance(),
            "Manhattan" => new ManhattanDistance(),
            _ => throw new InvalidOperationException("Choix d'algorithme de distance invalide")
        };
    }

    private ISortAlgo AskSortingAlgorithm()
    {
        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choisissez un [green]algorithme de tri[/]:")
                .AddChoices("Tri à bulle", "Tri fusion")
        );

        return choice switch
        {
            "Tri à bulle" => new BubbleSort(),
            "Tri fusion" => new MergeSort(),
            _ => throw new InvalidOperationException("Choix d'algorithme de tri invalide")
        };
    }

    private string AskFilePath(string description)
    {
        string? path = null;
        while (string.IsNullOrEmpty(path) || !File.Exists(path))
        {
            path = AnsiConsole.Ask<string>($"Entrez le chemin pour le [yellow]{description}[/]:");
            if (!File.Exists(path))
            {
                AnsiConsole.MarkupLine("[red]Fichier non trouvé. Veuillez entrer un chemin valide.[/]");
            }
        }
        return path;
    }

    private void ProcessKnn(int k, IDistance distanceAlgo, ISortAlgo sortAlgo, string trainPath, string testPath, string outputPath)
    {
        CsvManager csvManager = new CsvManager();
        SeedFactory seedFactory = new SeedFactory();
        ClassificationEvaluator evaluator = new ClassificationEvaluator();
        JsonManager jsonManager = new JsonManager();

        var dictTrainSeeds = csvManager.ReadCsv(trainPath);
        var dictTestSeeds = csvManager.ReadCsv(testPath);

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
        AnsiConsole.MarkupLine($"[green]Évaluation terminée! Résultats sauvegardés dans {outputPath}[/]");
    }
}