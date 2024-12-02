using Microsoft.Extensions.Logging;
using TextFilterApplication.Exceptions;

namespace TextFilterApplication.Repositories
{
    /// <summary>
    /// Filters words based on  char 't' criteria.
    /// </summary>
    public class LetterTFilterRepository : ITextFilterRepository
    {
        private readonly ILogger<LetterTFilterRepository> logger;

        public LetterTFilterRepository(ILogger<LetterTFilterRepository> logger)
        {
            this.logger = logger;
        }

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
                this.logger.LogError(ex, "An error occurred during filtering in LetterTFilterRepository.");
                throw new TextFilterException("An error occurred during filtering in LetterTFilterRepository.", ex);
            }
        }
    }
}
