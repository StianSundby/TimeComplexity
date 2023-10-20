using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using static uMethodLib.Tests;
using static uMethodLib.UtilityMethods;

namespace uMethodLib
{
    internal sealed class TestSuite : Command<TestSuite.Settings>
    {
        public sealed class Settings : CommandSettings
        {
            [Description("Which tests do you wish to run?\nAll tests are enabled by default, and ran against a dataset with a length of 1.000.000")]
            [CommandArgument(0, "[catalogue]")]
            public string? Catalogue { get; init; }

            [CommandOption("--search")]
            [DefaultValue(false)]
            public bool RunSearch { get; init; }

            [CommandOption("--sort")]
            [DefaultValue(false)]
            public bool RunSort { get; init; }

            [CommandOption("--path")]
            [DefaultValue(false)]
            public bool RunPath { get; init; }

            [CommandOption("--all")]
            [DefaultValue(true)]
            public bool RunAll { get; init; }

            [CommandOption("--datasetsize")]
            [DefaultValue(1000000)]
            public int DataSetSize { get; init; }
        }

        public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
        {
            AnsiConsole.Write(new Markup("[italic grey]\nAll tests are being ran against a dataset with a size of " + Convert.ToString(settings.DataSetSize) + "\n[/]"));
            AnsiConsole.Write(new Markup("[italic grey]Values on the chart are how many milliseconds the tests took to run\n\n[/]"));

            int n = settings.DataSetSize;

            if (settings.RunAll)
            {
                settings = new Settings
                {
                    RunSearch = true,
                    RunSort = true,
                    RunPath = true
                };
            }

            var results = new Dictionary<string, Dictionary<string, TimeSpan>>();

            if (settings.RunSearch)
            {
                //TODO: Clean this up somehow?
                results["Sorted Search"] = PerformSortedSearchTests(n).OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                results["Unsorted Search"] = PerformUnsortedSearchTests(n).OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                results["First Positive"] = PerformFirstPositiveTests(n).OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value); //TODO: somehow add a an option to the --search command settings to enable or disable this test

                //TODO: Implement Sublist Search (I have to make some dummy data for it to loop through)
                results["Sublist Search"] = PerformSublistSearchTests(n).OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value); //TODO: somehow add a an option to the --search command settings to enable or disable this test

            }

            //if (settings.RunSort)
            //{

            //}

            //if (settings.RunPath)
            //{

            //}

            RenderResults(results);
            return 0;
        }

        private static void RenderResults(Dictionary<string, Dictionary<string, TimeSpan>> results)
        {
            var failedTests = new Dictionary<string, Dictionary<string, TimeSpan>>();
            foreach (var (testName, testResults) in results)
            {
                var chart = new BarChart()
                    .Width(100)
                    .Label($"[green bold underline]\n{testName}[/]")
                    .CenterLabel();

                var gradient = GenerateGradient(testResults.Count);
                int colorIndex = 0;

                foreach (var (name, time) in testResults)
                {
                    if (time == TimeSpan.Zero)
                    {
                        if (!failedTests.ContainsKey(testName))
                            failedTests[testName] = new Dictionary<string, TimeSpan>();
                        failedTests[testName].Add(name, time);
                        continue;
                    }

                    chart.AddItem(name, time.TotalMilliseconds, gradient[colorIndex]);
                    colorIndex++;
                }

                AnsiConsole.Write(chart);
            }

            if (failedTests.Keys.Count == 0) return;

            AnsiConsole.Write(new Markup("[bold red underline]\n\nFailed tests\n[/]"));
            var table = new Table();
            table.AddColumn("Type");
            table.AddColumn("Algorithm");

            foreach (var (testName, testResults) in failedTests)
            {
                foreach (var (name, _) in testResults)
                    table.AddRow(testName, "[red]" + name + "[/]");
            }

            AnsiConsole.Write(table);
        }
    }
}
