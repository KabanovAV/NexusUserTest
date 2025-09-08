using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace NexusUserTest.Shared.NexusBlazor
{
    public abstract class NexusBaseInput<TValue> : ComponentBase
    {
        [CascadingParameter]
        protected EditContext? EditContext { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; } = [];

        [Parameter]
        public TValue? Value { get; set; }
        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }
        [Parameter]
        public Expression<Func<TValue>>? NexusValueExpression { get; set; }
        [Parameter]
        public string CssClass { get; set; } = "form-control";
        [Parameter]
        public string InvalidCssClass { get; set; } = "is-invalid";
        [Parameter]
        public bool ValidateOnInput { get; set; } = true;

        protected FieldIdentifier FieldIdentifier;
        protected bool HasValidationErrors => EditContext?.GetValidationMessages(FieldIdentifier).Any() == true;
        protected string CombinedCssClass => $"{CssClass} {(HasValidationErrors ? InvalidCssClass : "")}";

        protected TValue CurrentValue
        {
            get => Value!;
            set
            {
                if (EqualityComparer<TValue>.Default.Equals(Value, value)) return;

                Value = value;
                ValueChanged.InvokeAsync(value);

                if (ValidateOnInput && EditContext != null)
                    EditContext.NotifyFieldChanged(FieldIdentifier);
            }
        }

        protected override void OnInitialized()
        {
            if (NexusValueExpression != null)
                FieldIdentifier = FieldIdentifier.Create(NexusValueExpression);
        }

        public void ForceValidation()
            => EditContext?.NotifyFieldChanged(FieldIdentifier);
    }
}
