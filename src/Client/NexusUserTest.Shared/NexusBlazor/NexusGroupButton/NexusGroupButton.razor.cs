using Microsoft.AspNetCore.Components;

namespace NexusUserTest.Shared.NexusBlazor
{
    public partial class NexusGroupButton : ComponentBase
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string? CssClass { get; set; }
        [Parameter]
        public string? CssStyle { get; set; }
        [Parameter]
        public bool Visible { get; set; }
        [Parameter]
        public bool Vertical { get; set; }
    }
}
