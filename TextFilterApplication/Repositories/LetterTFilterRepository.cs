using TextFilterApplication.Exceptions;

namespace TextFilterApplication.Repositories
{
    public class LetterTFilterRepository : ITextFilterRepository
    {
        public string Apply(string input)
        {
            try
            {
                var words = input.Split(' ');
                var filteredWords = words.Where(word => !word.Contains('t'));
                return string.Join(" ", filteredWords);
            }
            catch (Exception ex)
            {
                throw new TextFilterException("An error occurred during filtering in VowelMiddleFilterRepository.", ex);
            }
        }
    }
}
