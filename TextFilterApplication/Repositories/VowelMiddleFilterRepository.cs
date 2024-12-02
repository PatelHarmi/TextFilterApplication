using System.Text.RegularExpressions;
using TextFilterApplication.Exceptions;

namespace TextFilterApplication.Repositories
{
    public class VowelMiddleFilterRepository : ITextFilterRepository
    {
        public string Apply(string input)
        {
            try
            {
                string sanitizedInput = Regex.Replace(input, @"[^\w\s]", string.Empty);
                sanitizedInput = Regex.Replace(sanitizedInput, @"\s+", " ").Trim();
                var words = sanitizedInput.Split(' ');

                var filteredWords = words.Where(word =>
                {
                    if (string.IsNullOrEmpty(word) || word.Length == 1)
                        return true;

                    int midIndex = word.Length / 2;

                    char[] middleChars = word.Length % 2 == 0
                        ? new[] { word[midIndex - 1], word[midIndex] }
                        : new[] { word[midIndex] };

                    return !middleChars.Any(c => "aeiou".Contains(char.ToLower(c)));
                });

                return string.Join(" ", filteredWords);
            }
            catch (Exception ex)
            {
                throw new TextFilterException("An error occurred during filtering in VowelMiddleFilterRepository.", ex);
            }
        }
    }
}
