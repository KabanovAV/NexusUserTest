using Microsoft.AspNetCore.Components;

namespace NexusUserTest.Shared.NexusBlazor
{
    public partial class NexusTableGrid<TItem> : ComponentBase
    {
        [Parameter, EditorRequired]
        public List<TItem> Data { get; set; }

        [Parameter]
        public bool ShowEmptyMessage { get; set; }
        [Parameter]
        public string? EmptyMessage { get; set; }
        [Parameter]
        public bool AllowSorting { get; set; }
        [Parameter]
        public bool AllowFiltering { get; set; }
        [Parameter]
        public string? CssClass { get; set; }
        [Parameter]
        public string? CssStyle { get; set; }
        [Parameter]
        public RenderFragment? ToolTemplate { get; set; }
        [Parameter]
        public RenderFragment? Columns { get; set; }
        [Parameter]
        public RenderFragment<TItem>? EditColumns { get; set; }
        [Parameter]
        public RenderFragment<TItem>? ExpandRow { get; set; }

        [Parameter]
        public NexusTableGridSelectionMode SelectionMode { get; set; }
        [Parameter]
        public NexusTableGridExpandMode ExpandMode { get; set; }
        [Parameter]
        public NexusTableGridEditMode EditMode { get; set; }

        [Parameter]
        public EventCallback OnSelectRow { get; set; }
        [Parameter]
        public EventCallback<TItem> OnRowSelected { get; set; }
        [Parameter]
        public EventCallback<TItem> OnRowExpanded { get; set; }
        [Parameter]
        public EventCallback<TItem> OnRowEdited { get; set; }
        [Parameter]
        public EventCallback<TItem> RowDoubleClick { get; set; }

        private NexusTableGridState<TItem>? _state;
        private List<NexusTableGridColumn<TItem>> _items = [];
        public List<NexusTableGridColumn<TItem>> Items
        {
            get => _items;
            set => _items = value;
        }
        private int ColSpan => ExpandMode != NexusTableGridExpandMode.None ? Items.Count + 1 : Items.Count;

        public HashSet<TItem> SelectedRows { get; set; } = [];
        public HashSet<TItem> ExpandedRows { get; set; } = [];
        public HashSet<TItem> InsertedItems { get; set; } = [];
        public HashSet<TItem> EditedItems { get; set; } = [];

        private bool IsInsertMode { get; set; }
        private bool IsEditMode { get; set; }
        private bool IsExpnadMode => ExpandMode != NexusTableGridExpandMode.None;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            _state = new NexusTableGridState<TItem>(Data);
        }

        /// <summary>
        /// Сортировка столбца
        /// </summary>
        /// <param name="column">Название столбца</param>
        private void SortBy(string column)
            => _state?.SortBy(column);

        /// <summary>
        /// Применение фильтра
        /// </summary>
        /// <param name="column">Название столбца</param>
        /// <param name="value">Значение</param>
        private void SetFilter(string column, string? value)
            => _state?.SetFilter(column, value);

        /// <summary>
        /// Добавление колонки в таблицу
        /// </summary>
        /// <param name="item">Колонка</param>
        public void AddItem(NexusTableGridColumn<TItem> item)
        {
            if (!_items.Contains(item))
            {
                _items.Add(item);
                Refresh();
            }
        }

        /// <summary>
        /// Выбор строки
        /// </summary>
        /// <param name="item">Строка</param>
        public async Task SelectRow(TItem item)
        {
            if (SelectionMode == NexusTableGridSelectionMode.Single)
            {
                SelectedRows.Clear();
                SelectedRows?.Add(item);
            }
            else if (SelectionMode == NexusTableGridSelectionMode.Multiple)
            {
                if (!SelectedRows.Add(item))
                    SelectedRows.Remove(item);
            }
            await OnSelectRow.InvokeAsync();
            await OnRowSelected.InvokeAsync(item);
        }

        /// <summary>
        /// Событие двойного нажатие мышки на строку
        /// </summary>
        /// <param name="args">Строка</param>
        public async Task OnRowDblClick(TItem args)
            => await RowDoubleClick.InvokeAsync(args);

        /// <summary>
        /// Признак выбранной строки
        /// </summary>
        public bool IsRowsSelected => SelectedRows.Count != 0;

        /// <summary>
        /// Признак выбранной конкретной строки
        /// </summary>
        /// <param name="item">Строка</param>
        public bool IsRowSelected(TItem item) => SelectedRows.Contains(item);

        public async Task OnExpandRow(TItem item)
        {
            if (ExpandedRows.Contains(item))
                ExpandedRows.Remove(item);
            else
            {
                if (ExpandMode == NexusTableGridExpandMode.Single)
                    ExpandedRows.Clear();
                ExpandedRows.Add(item);
            }
            await OnRowExpanded.InvokeAsync(item);
        }

        public async Task InsertRow(TItem item)
        {
            Data.Add(item);
            InsertedItems?.Add(item);
            await SelectRow(item);
            EditRow(item);
            IsInsertMode = true;
        }

        public void EditRow(TItem item)
        {
            if (EditMode == NexusTableGridEditMode.Single)
            {
                EditedItems.Clear();
                EditedItems.Add(item);
            }
            else
            {
                if (!EditedItems.Contains(item))
                    EditedItems.Add(item);
            }
            IsEditMode = true;
            OnRowEdited.InvokeAsync(item);
        }

        public void RemoveRow(TItem item)
        {
            ExpandedRows.Remove(item);
            Data.Remove(item);
            SelectedRows.Remove(item);
            EditedItems.Remove(item);
            InsertedItems.Remove(item);
        }

        public async Task CancelEditRow(TItem item)
        {
            if (EditedItems.Remove(item))
                IsEditMode = EditedItems.Count != 0;
            if (InsertedItems.Remove(item))
            {
                Data.Remove(item);
                if (EditMode == NexusTableGridEditMode.Multiple && InsertedItems.Count > 0)
                    await SelectRow(InsertedItems.Last());
                else
                    SelectedRows.Remove(item);
                IsInsertMode = InsertedItems.Count != 0;
            }
        }

        public async Task Reload()
        {
            foreach (var item in InsertedItems)
                Data.Remove(item);
            InsertedItems.Clear();
            EditedItems.Clear();
            IsInsertMode = false;
            IsEditMode = false;
            await InvokeAsync(StateHasChanged);
        }

        public void ClearSelection()
        {
            SelectedRows.Clear();
            Refresh();
        }

        public void CollapseAll()
        {
            ExpandedRows.Clear();
            Refresh();
        }

        public void Refresh() => StateHasChanged();
        public async Task RefreshAsync() => await InvokeAsync(StateHasChanged);
    }
}
