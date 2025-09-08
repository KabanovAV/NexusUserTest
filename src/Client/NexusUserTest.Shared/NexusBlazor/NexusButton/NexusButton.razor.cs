using Microsoft.AspNetCore.Components;

namespace NexusUserTest.Shared.NexusBlazor
{
    public partial class NexusButton : ComponentBase
    {
        [Parameter]
        public string? Title { get; set; }
        [Parameter]
        public string? CssClass { get; set; }
        [Parameter]
        public string? CssStyle { get; set; }
        [Parameter]
        public string? IconClass { get; set; }
        [Parameter]
        public bool Disable { get; set; }
        [Parameter]
        public EventCallback OnClick { get; set; }
    }
}
