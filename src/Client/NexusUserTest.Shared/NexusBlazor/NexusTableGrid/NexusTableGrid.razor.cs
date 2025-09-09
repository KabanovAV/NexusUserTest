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

        private List<NexusTableGridColumn<TItem>> _items = [];
        public List<NexusTableGridColumn<TItem>> Items
        {
            get => _items;
            set => _items = value;
        }
        private int ColSpan => ExpandMode != NexusTableGridExpandMode.None ? Items.Count + 1 : Items.Count;

        public List<TItem> SelectedRows { get; set; } = [];
        public List<TItem> ExpandedRows { get; set; } = [];
        public List<TItem> InsertItem { get; set; } = [];
        public List<TItem> EditedItem { get; set; } = [];

        private bool IsInsertMode { get; set; }
        private bool IsEditMode { get; set; }
        private bool IsExpnadMode => ExpandMode != NexusTableGridExpandMode.None;

        public void AddItem(NexusTableGridColumn<TItem> item)
        {
            if (_items.IndexOf(item) == -1)
            {
                _items.Add(item);
                Refresh();
            }
        }

        public async Task SelectRow(TItem item)
        {
            if (SelectionMode == NexusTableGridSelectionMode.Single)
            {
                SelectedRows.Clear();
                SelectedRows?.Add(item);
            }
            else
            {
                if (SelectedRows.Contains(item))
                    SelectedRows.Remove(item);
                else
                    SelectedRows.Add(item);
            }
            await OnSelectRow.InvokeAsync();
        }

        public bool IsRowsSelected
            => SelectedRows.Count != 0;

        public bool IsRowSelected(TItem item)
            => SelectedRows.Contains(item);

        public async Task OnExpandRow(TItem item)
        {
            if (ExpandedRows.Contains(item))
                ExpandedRows.Remove(item);
            else
            {
                if (ExpandMode == NexusTableGridExpandMode.Single)
                {
                    ExpandedRows.Clear();
                    ExpandedRows?.Add(item);
                }
                else
                    ExpandedRows.Add(item);
            }

            await OnSelectRow.InvokeAsync();
        }

        public async Task InsertRow(TItem item)
        {
            Data.Add(item);
            InsertItem?.Add(item);
            await SelectRow(item);
            EditRow(item);
            IsInsertMode = true;
        }

        public void EditRow(TItem item)
        {
            if (EditMode == NexusTableGridEditMode.Single)
            {
                EditedItem.Clear();
                EditedItem.Add(item);
            }
            else
            {
                if (!EditedItem.Contains(item))
                    EditedItem.Add(item);
            }
            IsEditMode = true;
        }

        public void RemoveRow(TItem item)
        {
            if (ExpandedRows.Contains(item))
                ExpandedRows.Remove(item);
            Data.Remove(item);
            SelectedRows.Remove(item);
        }

        public async Task CancelEditRow(TItem item)
        {
            if (EditedItem.Contains(item))
            {
                EditedItem.Remove(item);
                IsEditMode = EditedItem.Count != 0;
            }
            if (InsertItem.Contains(item))
            {
                Data.Remove(item);
                InsertItem.Remove(item);
                if (EditMode == NexusTableGridEditMode.Multiple && InsertItem.Count > 0)
                    await SelectRow(InsertItem.Last());
                else
                    SelectedRows.Remove(item);
                IsInsertMode = InsertItem.Count != 0;
            }
        }

        public async Task Reload()
        {
            if (InsertItem.Count > 0)
            {
                foreach (var item in InsertItem)
                    Data.Remove(item);
            }
            InsertItem.Clear();
            EditedItem.Clear();
            IsInsertMode = false;
            IsEditMode = false;
            await InvokeAsync(StateHasChanged);
        }

        public void Refresh() => StateHasChanged();
        public async Task RefreshAsync() => await InvokeAsync(StateHasChanged);
    }
}
