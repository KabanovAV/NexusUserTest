using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections;

namespace NexusUserTest.Shared.NexusBlazor
{
    public partial class NexusDropDown<TValue>
    {
        [Parameter, EditorRequired] public IEnumerable Items { get; set; }
        [Parameter] public string? TextField { get; set; }
        [Parameter] public string? ValueField { get; set; }
        [Parameter] public string Placeholder { get; set; } = "-- Select --";

        protected override void OnParametersSet()
        {
            // Добавляем стандартные атрибуты если нужно
            if (!AdditionalAttributes.ContainsKey("id"))
                AdditionalAttributes["id"] = FieldIdentifier.FieldName;
        }

        private string GetItemText(object item)
        {
            if (item == null) return string.Empty;

            if (string.IsNullOrEmpty(TextField))
                return item.ToString();

            var property = item.GetType().GetProperty(TextField);
            return property?.GetValue(item)?.ToString() ?? string.Empty;
        }

        private TValue GetItemValue(object item)
        {
            if (item == null) return default!;

            if (string.IsNullOrEmpty(ValueField))
                return (TValue)item;

            var property = item.GetType().GetProperty(ValueField);
            var value = property?.GetValue(item);

            if (value is TValue typedValue)
                return typedValue;

            // Конвертация типов если необходимо
            try
            {
                return (TValue)Convert.ChangeType(value!, typeof(TValue));
            }
            catch
            {
                return default!;
            }
        }
    }
}
