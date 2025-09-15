namespace NexusUserTest.Shared.NexusBlazor
{
    public class NexusTableGridState<TItem>
    {
        private readonly List<TItem> Source;
        private List<TItem> Filtered { get; set; } = [];
        private Dictionary<string, string?> Filters = [];

        public string? SortColumn;
        public bool SortAscending = true;
        public List<TItem> View { get; private set; } = [];

        public NexusTableGridState(List<TItem> source)
        {
            Source = source;
            Apply();
        }

        public void SortBy(string column)
        {
            if (SortColumn == column)
                SortAscending = !SortAscending;
            else
            {
                SortColumn = column;
                SortAscending = true;
            }
            Apply();
        }

        public void SetFilter(string column, string? value)
        {
            Filters[column] = value;
            Apply();
        }

        private void Apply()
        {
            IEnumerable<TItem> query = Source;

            // Фильтрация
            foreach (var filter in Filters.Where(f => !string.IsNullOrEmpty(f.Value)))
            {
                query = query.Where(item =>
                {
                    var prop = typeof(TItem).GetProperty(filter.Key);
                    var val = prop?.GetValue(item)?.ToString();
                    return val?.Contains(filter.Value!, StringComparison.OrdinalIgnoreCase) ?? false;
                });
            }

            // Сортировка
            if (!string.IsNullOrEmpty(SortColumn))
            {
                var prop = typeof(TItem).GetProperty(SortColumn);
                if (prop != null)
                {
                    query = SortAscending
                        ? query.OrderBy(x => prop.GetValue(x))
                        : query.OrderByDescending(x => prop.GetValue(x));
                }
            }
            Filtered = [.. query];
            View = Filtered;
        }
    }
}
