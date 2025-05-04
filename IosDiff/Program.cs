using CommandLine;
using CommandLine.Text;
using System.Reflection;

namespace IosDiff;

internal class Program
{
    private static int Main(string[] args)
    {
        var parser = new Parser(settings =>
        {
            settings.HelpWriter = null;
            settings.AutoHelp = false;
            settings.AutoVersion = false;   
        });

        var result = parser.ParseArguments<
            UniqueToFirstOptions,
            UniqueToSecondOptions,
            CommonToBothOptions,
            DiffOptions,
            MergeOptions,
            PatchOptions
        >(args);

        return result.MapResult(
            (UniqueToFirstOptions options) => Runner.RunUniqueToFirst(options),
            (UniqueToSecondOptions options) => Runner.RunUniqueToSecond(options),
            (CommonToBothOptions options) => Runner.RunCommonToBoth(options),
            (DiffOptions options) => Runner.RunDiff(options),
            (MergeOptions options) => Runner.RunMerge(options),
            (PatchOptions options) => Runner.RunPatch(options),
            errs =>
            {
                ShowGlobalHelp(result);
                return 1;
            }
        );
    }

    private static void ShowGlobalHelp<T>(ParserResult<T> parserResult)
    {
        var help = HelpText.AutoBuild(
            parserResult,
            helpText =>
            {
                var assembly = Assembly.GetExecutingAssembly();
                helpText.AdditionalNewLineAfterOption = false;
                helpText.AddDashesToOption = false;
                helpText.AutoHelp = false;
                helpText.AutoVersion = false;
                helpText.MaximumDisplayWidth = 110;
                helpText.Copyright = "";
                helpText.Heading = $"{assembly.GetName().Name} {assembly.GetName().Version} Copyright 2025 Michael Crino";

                helpText.AddPreOptionsLine("\t");
                helpText.AddPreOptionsLine("Compares and transforms IOS configuration files.");
                helpText.AddPreOptionsLine("\t");
                helpText.AddPreOptionsLine($"Usage: {assembly.GetName().Name} <mode> [-c|--no-color] [-r|--raw] [-o|--outputFile <output file>] <first file> <second file>");

                helpText.AddPostOptionsLine("This software is provided \"as is\", without any warranty, express or implied, including but not limited to the implied warranties of merchantability or fitness for a particular purpose.");
                helpText.AddPostOptionsLine("");
                helpText.AddPostOptionsLine("Licensed under the GNU General Public License v3. See https://www.gnu.org/licenses/gpl-3.0.html for terms.");
                return HelpText.DefaultParsingErrorsHandler(parserResult, helpText);
            },
            example => example
        );

        help.AddVerbs(
            typeof(UniqueToFirstOptions),
            typeof(UniqueToSecondOptions),
            typeof(CommonToBothOptions),
            typeof(DiffOptions),
            typeof(MergeOptions),
            typeof(PatchOptions)
        );

        Console.WriteLine(help);
    }
    internal class OptionsBase
    {
        [Value(0, MetaName = "firstFile", HelpText = "First file", Required = true)]
        public string FirstFile { get; set; }

        [Value(1, MetaName = "secondFile", HelpText = "Second file", Required = true)]
        public string SecondFile { get; set; }

        [Option('o', "outputFile", HelpText = "Output file", Required = false)]
        public string? OutputFile { get; set; }
    }

    [Verb("common", HelpText = "Show lines common to both files")]
    internal class CommonToBothOptions : OptionsBase;

    [Verb("diff", HelpText = "Show differences between the first and second file")]
    internal class DiffOptions : OptionsBase
    {
        [Option('r', "raw", HelpText = "Outputs the raw configuration without any formating", Required = false, Default = false)]
        public bool Raw { get; set; }

        [Option('c', "no-color", HelpText = "Colorizes configuration lines", Required = false)]
        public bool NoColor { get; set; }
    }

    [Verb("merge", HelpText = "Generates a configuration merge")]
    internal class MergeOptions : OptionsBase
    {
        [Option('r', "raw", HelpText = "Outputs the raw configuration without any formating", Required = false, Default = false)]
        public bool Raw { get; set; }

        [Option('c', "no-color", HelpText = "Colorizes configuration lines", Required = false)]
        public bool NoColor { get; set; }
    }

    [Verb("first", HelpText = "Show lines unique to the first file")]
    internal class UniqueToFirstOptions : OptionsBase
    {
        [Option('r', "raw", HelpText = "Outputs the raw configuration without any formating", Required = false, Default = false)]
        public bool Raw { get; set; }

    [Option('c', "no-color", HelpText = "Colorizes configuration lines", Required = false)]
    public bool NoColor { get; set; }
}

    [Verb("second", HelpText = "Show lines unique to the second file")]
    internal class UniqueToSecondOptions : OptionsBase
    {
        [Option('r', "raw", HelpText = "Outputs the raw configuration without any formating", Required = false, Default = false)]
        public bool Raw { get; set; }

        [Option('c', "no-color", HelpText = "Colorizes configuration lines", Required = false)]
        public bool NoColor { get; set; }
    }

    [Verb("patch", HelpText = "Generate commands to transform the first configuration into the second configuration")]
    internal class PatchOptions : OptionsBase
    {
        [Option('r', "raw", HelpText = "Outputs the raw configuration without any formating", Required = false, Default = false)]
        public bool Raw { get; set; }

        [Option('c', "no-color", HelpText = "Colorizes configuration lines", Required = false)]
        public bool NoColor { get; set; }
    }
}