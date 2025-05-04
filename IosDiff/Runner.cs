using Bitvantage.Cisco;

namespace IosDiff;

internal static class Runner
{
    internal static int RunCommonToBoth(Program.CommonToBothOptions options)
    {
        var file1 = File.ReadAllText(options.FirstFile);
        var file2 = File.ReadAllText(options.SecondFile);

        var section1 = ConfigurationSection.Parse(file1);
        var section2 = ConfigurationSection.Parse(file2);

        var result = ConfigurationSection.Compare(section1, section2).CommonToBoth();

        if (options.OutputFile != null)
            File.WriteAllText(options.OutputFile, result.Section.ToString());
        else
            Console.Write(result.Section);

        return 0;
    }

    internal static int RunDiff(Program.DiffOptions options)
    {
        var file1 = File.ReadAllText(options.FirstFile);
        var file2 = File.ReadAllText(options.SecondFile);

        var section1 = ConfigurationSection.Parse(file1);
        var section2 = ConfigurationSection.Parse(file2);

        var result = ConfigurationSection.Compare(section1, section2).Differences();


        if (options.OutputFile != null)
            switch (options)
            {
                case { Raw: true }:
                    File.WriteAllText(options.OutputFile, result.Section.ToString());
                    break;

                case { Raw: false }:
                    File.WriteAllText(options.OutputFile, result.Format("> ", "< ", "  ", ""));
                    break;
            }
        else
            switch (options)
            {
                case { NoColor: true, Raw: true }:
                    Console.Write(result.Section);
                    break;

                case { NoColor: true, Raw: false }:
                    Console.Write(result.Format("> ", "< ", "  ", ""));
                    break;

                case { NoColor: false, Raw: true }:
                    Console.Write(result.Format("\u001b[32m", "\u001b[31m", "\u001b[0m", "\u001b[0m"));
                    break;

                case { NoColor: false, Raw: false }:
                    Console.Write(result.Format("\u001b[32m> ", "\u001b[31m< ", "\u001b[0m  ", "\u001b[0m"));
                    break;

                default:
                    throw new ArgumentException();
            }

        return 0;
    }

    internal static int RunMerge(Program.MergeOptions options)
    {
        var file1 = File.ReadAllText(options.FirstFile);
        var file2 = File.ReadAllText(options.SecondFile);

        var section1 = ConfigurationSection.Parse(file1);
        var section2 = ConfigurationSection.Parse(file2);

        var result = ConfigurationSection.Compare(section1, section2).Merged;

        if (options.OutputFile != null)
            switch (options)
            {
                case { Raw: true }:
                    File.WriteAllText(options.OutputFile, result.Section.ToString());
                    break;

                case { Raw: false }:
                    File.WriteAllText(options.OutputFile, result.Format("> ", "< ", "  ", ""));
                    break;
            }
        else
            switch (options)
            {
                case { NoColor: true, Raw: true }:
                    Console.Write(result.Section);
                    break;

                case { NoColor: true, Raw: false }:
                    Console.Write(result.Format("> ", "< ", "  ", ""));
                    break;

                case { NoColor: false, Raw: true }:
                    Console.Write(result.Format("\u001b[32m", "\u001b[31m", "\u001b[0m", "\u001b[0m"));
                    break;

                case { NoColor: false, Raw: false }:
                    Console.Write(result.Format("\u001b[32m> ", "\u001b[31m< ", "\u001b[0m  ", "\u001b[0m"));
                    break;

                default:
                    throw new ArgumentException();
            }

        return 0;
    }

    internal static int RunPatch(Program.PatchOptions options)
    {
        var file1 = File.ReadAllText(options.FirstFile);
        var file2 = File.ReadAllText(options.SecondFile);

        var section1 = ConfigurationSection.Parse(file1);
        var section2 = ConfigurationSection.Parse(file2);

        var result = ConfigurationSection.Compare(section1, section2).Patch();

        if (options.OutputFile != null)
            switch (options)
            {
                case { Raw: true }:
                    File.WriteAllText(options.OutputFile, result.Section.ToString());
                    break;

                case { Raw: false }:
                    File.WriteAllText(options.OutputFile, result.Format("> ", "< ", "  ", ""));
                    break;
            }
        else
            switch (options)
            {
                case { NoColor: true, Raw: true }:
                    Console.Write(result.Section);
                    break;

                case { NoColor: true, Raw: false }:
                    Console.Write(result.Format("> ", "< ", "  ", ""));
                    break;

                case { NoColor: false, Raw: true }:
                    Console.Write(result.Format("\u001b[32m", "\u001b[31m", "\u001b[0m", "\u001b[0m"));
                    break;

                case { NoColor: false, Raw: false }:
                    Console.Write(result.Format("\u001b[32m> ", "\u001b[31m< ", "\u001b[0m  ", "\u001b[0m"));
                    break;

                default:
                    throw new ArgumentException();
            }

        return 0;
    }

    internal static int RunUniqueToFirst(Program.UniqueToFirstOptions options)
    {
        var file1 = File.ReadAllText(options.FirstFile);
        var file2 = File.ReadAllText(options.SecondFile);

        var section1 = ConfigurationSection.Parse(file1);
        var section2 = ConfigurationSection.Parse(file2);

        var result = ConfigurationSection.Compare(section1, section2).UniqueToFirst();

        if (options.OutputFile != null)
            switch (options)
            {
                case { Raw: true }:
                    File.WriteAllText(options.OutputFile, result.Section.ToString());
                    break;

                case { Raw: false }:
                    File.WriteAllText(options.OutputFile, result.Format("- ", "+ ", "  ", ""));
                    break;
            }
        else
            switch (options)
            {
                case { NoColor: true, Raw: true }:
                    Console.Write(result.Section);
                    break;

                case { NoColor: true, Raw: false }:
                    Console.Write(result.Format("- ", "+ ", "  ", ""));
                    break;

                case { NoColor: false, Raw: true }:
                    Console.Write(result.Format("\u001b[31m", "\u001b[32m", "\u001b[0m", "\u001b[0m"));
                    break;

                case { NoColor: false, Raw: false }:
                    Console.Write(result.Format("\u001b[31m- ", "\u001b[32m+ ", "\u001b[0m  ", "\u001b[0m"));
                    break;

                default:
                    throw new ArgumentException();
            }

        return 0;
    }

    internal static int RunUniqueToSecond(Program.UniqueToSecondOptions options)
    {
        var file1 = File.ReadAllText(options.FirstFile);
        var file2 = File.ReadAllText(options.SecondFile);

        var section1 = ConfigurationSection.Parse(file1);
        var section2 = ConfigurationSection.Parse(file2);

        var result = ConfigurationSection.Compare(section1, section2).UniqueToSecond();

        if (options.OutputFile != null)
            switch (options)
            {
                case { Raw: true }:
                    File.WriteAllText(options.OutputFile, result.Section.ToString());
                    break;

                case { Raw: false }:
                    File.WriteAllText(options.OutputFile, result.Format("+ ", "- ", "  ", ""));
                    break;
            }
        else
            switch (options)
            {
                case { NoColor: true, Raw: true }:
                    Console.Write(result.Section);
                    break;

                case { NoColor: true, Raw: false }:
                    Console.Write(result.Format("+ ", "- ", "  ", ""));
                    break;

                case { NoColor: false, Raw: true }:
                    Console.Write(result.Format("\u001b[32m", "\u001b[31m", "\u001b[0m", "\u001b[0m"));
                    break;

                case { NoColor: false, Raw: false }:
                    Console.Write(result.Format("\u001b[32m+ ", "\u001b[31m- ", "\u001b[0m  ", "\u001b[0m"));
                    break;

                default:
                    throw new ArgumentException();
            }

        return 0;
    }
}