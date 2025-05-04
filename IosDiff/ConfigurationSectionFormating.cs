using System.Text;
using Bitvantage.Cisco;

namespace IosDiff
{
    internal static class ConfigurationSectionFormating
    {
        public static string Format(this ComparisionContext content, string addPrefix, string removePrefix, string commonPrefix, string done)
        {
            var stringBuilder = new StringBuilder();

            foreach (var section in content.Section.Descendants())
            {
                var type = content.SectionMatch[section];
                var linePrefix = type switch
                {
                    SectionMembership.First => removePrefix,
                    SectionMembership.Second => addPrefix,
                    SectionMembership.First | SectionMembership.Second => commonPrefix,
                    _ => throw new ArgumentOutOfRangeException()
                };

                var stringReader = new StringReader(section.Line!);

                string? line;
                while ((line = stringReader.ReadLine()) != null)
                    stringBuilder.AppendLine($"{linePrefix}{new string(' ', section.Depth - 1)}{line}");
            }

            stringBuilder.Append(done);

            return stringBuilder.ToString();
        }

    }
}
