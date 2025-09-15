using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace NexusUserTest.Shared.NexusBlazor
{
    public partial class NexusTableGridColumn<TItem> : ComponentBase
    {
        [Parameter]
        public string Title { get; set; } = string.Empty;
        [Parameter]
        public Expression<Func<TItem, object?>>? Property { get; set; }
        [Parameter]
        public string? CssClass { get; set; }
        [Parameter]
        public string? CssStyle { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public bool Sortable { get; set; }
        [Parameter]
        public bool Filterable { get; set; }
        [Parameter]
        public RenderFragment<TItem>? EditTemplate { get; set; }
        [Parameter]
        public RenderFragment<TItem>? CellTemplate { get; set; }
        [Parameter]
        public RenderFragment? HeaderTemplate { get; set; }

        NexusTableGrid<TItem> _tableGrid;

        [CascadingParameter]
        public NexusTableGrid<TItem> TableGrid
        {
            get => _tableGrid;
            set
            {
                if (_tableGrid != value)
                {
                    _tableGrid = value;
                    _tableGrid.AddItem(this);
                }
            }
        }

        public Func<TItem, object?>? Field => Property?.Compile();
        public string? FieldName { get; private set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (Property != null)
                FieldName = GetMemberPath(Property);
        }

        private static string? GetMemberPath(Expression<Func<TItem, object?>> expr)
        {
            Expression body = expr.Body;
            // убрать Convert если есть (когда TProp приводится к object)
            if (body is UnaryExpression unary && unary.NodeType == ExpressionType.Convert)
                body = unary.Operand;

            var members = new List<string>();
            while (body is MemberExpression member)
            {
                members.Add(member.Member.Name);
                body = member.Expression!;
            }
            members.Reverse();
            return members.Count > 0 ? string.Join(".", members) : null;
        }
    }
}
