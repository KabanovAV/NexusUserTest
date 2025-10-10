using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace NexusUserTest.Shared.NexusBlazor
{
    public partial class NexusPassword
    {
        [Parameter]
        public string? Placeholder { get; set; }
        [Parameter]
        public int MaxLength { get; set; } = 524288;

        protected string CurrentValueAsString
        {
            get => Value!;
            set => CurrentValue = value;
        }

        protected override void OnParametersSet()
        {
            // Добавляем placeholder если указан
            if (!string.IsNullOrEmpty(Placeholder) && !AdditionalAttributes.ContainsKey("placeholder"))
                AdditionalAttributes["placeholder"] = Placeholder;
        }

        private void HandleInput(string? inputValue)
        {
            if (!string.IsNullOrEmpty(inputValue))
                CurrentValueAsString = inputValue;
        }

        private void HandleBlur(FocusEventArgs e)
        {
            // Форсируем валидацию при потере фокуса
            if (EditContext != null && NexusValueExpression != null)
                EditContext.NotifyFieldChanged(FieldIdentifier);
            this.StateHasChanged();
        }
    }
}
