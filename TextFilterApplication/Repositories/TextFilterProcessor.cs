﻿namespace TextFilterApplication.Repositories
{
    public class TextFilterProcessor
    {
        private readonly IEnumerable<ITextFilterRepository> filters;

        public TextFilterProcessor(IEnumerable<ITextFilterRepository> filters)
        {
            this.filters = filters;
        }

        public string ApplyFilters(string input)
        {
            foreach (var filter in this.filters)
            {
                input = filter.Apply(input);
            }

            return input;
        }
    }
}
