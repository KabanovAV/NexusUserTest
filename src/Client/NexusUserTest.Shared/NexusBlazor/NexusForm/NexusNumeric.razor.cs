using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System.Globalization;

namespace NexusUserTest.Shared.NexusBlazor
{
    public partial class NexusNumeric<TValue>
    {
        [Parameter] public TValue? Min { get; set; }
        [Parameter] public TValue? Max { get; set; }
        [Parameter] public string? Step { get; set; } = "any";
        [Parameter] public string? Placeholder { get; set; }
        [Parameter] public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;

        private string _inputBuffer = string.Empty;
        private bool _isEditing;
        private bool _isIntegerType;
        private bool _isDecimalType;

        protected string CurrentValueAsString
        {
            get => _isEditing ? _inputBuffer : FormatValue(Value!);
            set
            {
                _inputBuffer = value;
                if (TryParseValue(value, out var parsedValue))
                    CurrentValue = parsedValue;
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            DetermineType();
        }

        protected override void OnParametersSet()
        {
            var type = typeof(TValue);
            var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

            // Добавляем min/max атрибуты как data-атрибуты для JavaScript валидации
            if (!EqualityComparer<TValue>.Default.Equals(Min, default) && !AdditionalAttributes.ContainsKey("data-min"))
                AdditionalAttributes["data-min"] = Min!;
            if (!EqualityComparer<TValue>.Default.Equals(Max, default) && !AdditionalAttributes.ContainsKey("data-max"))
                AdditionalAttributes["data-max"] = Max!;

            // Добавляем placeholder если указан
            if (!string.IsNullOrEmpty(Placeholder) && !AdditionalAttributes.ContainsKey("placeholder"))
                AdditionalAttributes["placeholder"] = Placeholder;

            // Устанавливаем тип input в зависимости от числового типа
            if (underlyingType == typeof(int) || underlyingType == typeof(long) ||
                underlyingType == typeof(short) || underlyingType == typeof(byte))
            {
                AdditionalAttributes["inputmode"] = "numeric";
                AdditionalAttributes["pattern"] = "[0-9]*";
            }
            else
            {
                AdditionalAttributes["inputmode"] = "decimal";
                AdditionalAttributes["pattern"] = "[0-9.,]*";
            }
        }

        private void DetermineType()
        {
            var type = typeof(TValue);
            var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

            _isIntegerType = underlyingType == typeof(int) ||
                            underlyingType == typeof(long) ||
                            underlyingType == typeof(short) ||
                            underlyingType == typeof(byte) ||
                            underlyingType == typeof(sbyte) ||
                            underlyingType == typeof(uint) ||
                            underlyingType == typeof(ulong) ||
                            underlyingType == typeof(ushort);

            _isDecimalType = underlyingType == typeof(decimal) ||
                            underlyingType == typeof(double) ||
                            underlyingType == typeof(float);
        }

        private void HandleInput(string inputValue)
        {
            _isEditing = true;
            if (ValidateInput(inputValue, out var validatedValue))
            {
                _inputBuffer = validatedValue;
                if (TryParseValue(inputValue, out var parsedValue))
                    CurrentValueAsString = parsedValue.ToString();
            }
            else
                CurrentValueAsString = _inputBuffer;
        }

        private void HandleBlur(FocusEventArgs e)
        {
            _isEditing = false;
            // Форсируем валидацию при потере фокуса
            if (EditContext != null && NexusValueExpression != null)
                EditContext.NotifyFieldChanged(FieldIdentifier);
            this.StateHasChanged();
        }

        private bool ValidateInput(string input, out string validatedValue)
        {
            validatedValue = input;

            // Для целых чисел запрещаем разделители
            if (_isIntegerType && (input.Contains('.') || input.Contains(',')))
                return false;

            // Для decimal чисел разрешаем только один разделитель
            if (_isDecimalType)
            {
                var dotCount = input.Count(c => c == '.');
                var commaCount = input.Count(c => c == ',');

                if (dotCount > 1 || commaCount > 1 || (dotCount > 0 && commaCount > 0))
                    return false;

                // Заменяем запятую на точку для consistency
                if (commaCount > 0)
                    validatedValue = input.Replace(',', '.');
            }
            // Проверяем, что остаются только допустимые символы
            var allowedChars = _isIntegerType ? "0123456789-" : "0123456789-,.";
            return input.All(c => allowedChars.Contains(c));
        }

        private string FormatValue(TValue value)
        {
            if (EqualityComparer<TValue>.Default.Equals(value, default))
                return string.Empty;

            return value switch
            {
                int intValue => intValue.ToString(Culture),
                decimal decimalValue => decimalValue.ToString(Culture),
                double doubleValue => doubleValue.ToString(Culture),
                float floatValue => floatValue.ToString(Culture),
                long longValue => longValue.ToString(Culture),
                short shortValue => shortValue.ToString(Culture),
                byte byteValue => byteValue.ToString(Culture),
                uint uintValue => uintValue.ToString(Culture),
                ulong ulongValue => ulongValue.ToString(Culture),
                ushort ushortValue => ushortValue.ToString(Culture),
                sbyte sbyteValue => sbyteValue.ToString(Culture),
                _ => string.Empty
            };
        }

        private bool TryParseValue(string value, out TValue result)
        {
            result = default!;

            if (string.IsNullOrEmpty(value))
                return true; // Разрешаем пустые значения

            // Разрешаем ввод точки и запятой как разделителей
            var normalizedValue = value.Replace(',', '.');

            try
            {
                var type = typeof(TValue);
                var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

                if (underlyingType == typeof(int) && int.TryParse(normalizedValue, NumberStyles.Any, Culture, out var intValue))
                {
                    result = (TValue)(object)intValue;
                    return true;
                }
                else if (underlyingType == typeof(long) && long.TryParse(normalizedValue, NumberStyles.Any, Culture, out var longValue))
                {
                    result = (TValue)(object)longValue;
                    return true;
                }
                else if (underlyingType == typeof(decimal) && decimal.TryParse(normalizedValue, NumberStyles.AllowDecimalPoint, Culture, out var decimalValue))
                {
                    result = (TValue)(object)decimalValue;
                    return true;
                }
                else if (underlyingType == typeof(double) && double.TryParse(normalizedValue, NumberStyles.Any, Culture, out var doubleValue))
                {
                    result = (TValue)(object)doubleValue;
                    return true;
                }
                else if (underlyingType == typeof(float) && float.TryParse(normalizedValue, NumberStyles.Float, Culture, out var floatValue))
                {
                    result = (TValue)(object)floatValue;
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
