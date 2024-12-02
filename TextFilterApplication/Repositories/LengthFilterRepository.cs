using Microsoft.Extensions.Logging;
using TextFilterApplication.Exceptions;

namespace TextFilterApplication.Repositories
{
    /// <summary>
    /// Filters words based on length criteria.
    /// </summary>
    public class LengthFilterRepository : ITextFilterRepository
    {
        private readonly ILogger<LengthFilterRepository> logger;

        public LengthFilterRepository(ILogger<LengthFilterRepository> logger)
        {
            this.logger = logger;
        }

        public string Apply(string input)
        {
            try
            {
                var words = input.Split(' ');
                var filteredWords = words.Where(word => word.Length >= 3);
                return string.Join(" ", filteredWords);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred during filtering in LengthFilterRepository.");
                throw new TextFilterException("An error occurred during filtering in LengthFilterRepository.", ex);
            }
        }
    }
}
