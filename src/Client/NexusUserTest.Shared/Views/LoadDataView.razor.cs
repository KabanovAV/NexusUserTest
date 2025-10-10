using Microsoft.AspNetCore.Components;
using NexusUserTest.Shared.Services;

namespace NexusUserTest.Shared.Views
{
    public partial class LoadDataView<TItem>
    {
        [Parameter]
        public ApiResponse<TItem>? Data { get; set; }
    }
}
