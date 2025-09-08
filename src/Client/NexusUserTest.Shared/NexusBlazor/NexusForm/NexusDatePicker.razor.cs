using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace NexusUserTest.Shared.NexusBlazor
{
    public partial class NexusDatePicker<TValue>
    {
        [Parameter] public NexusDateTimeInputMode Mode { get; set; } = NexusDateTimeInputMode.Date;
        [Parameter] public TValue? Min { get; set; }
        [Parameter] public TValue? Max { get; set; }
        [Parameter] public string? DateFormat { get; set; }
        [Parameter] public string? TimeFormat { get; set; }
        [Parameter] public string? DateTimeFormat { get; set; }
        [Parameter] public bool ShowSeconds { get; set; }
        [Parameter] public bool ShowMilliseconds { get; set; }

        private string? InputType;
        private string? CurrentValueAsString;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            InputType = DetermineType();
            CurrentValueAsString = FormatForInputType(Value!);
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            // Устанавливаем min/max атрибуты
            if (Min != null && !AdditionalAttributes.ContainsKey("min"))
                AdditionalAttributes["min"] = FormatForInputType(Min);
            if (Max != null && !AdditionalAttributes.ContainsKey("max"))
                AdditionalAttributes["max"] = FormatForInputType(Max);

            // Устанавливаем step для времени
            if (Mode == NexusDateTimeInputMode.Time && !AdditionalAttributes.ContainsKey("step"))
                AdditionalAttributes["step"] = ShowMilliseconds ? "0.001" : ShowSeconds ? "1" : "60";
        }

        private string FormatForInputType(TValue value)
        {
            if (EqualityComparer<TValue>.Default.Equals(value, default))
                return string.Empty;

            var dateTime = ConvertToDateTime(value);

            if (dateTime != null)
            {
                return Mode switch
                {
                    NexusDateTimeInputMode.Date => dateTime.Value.ToString(string.IsNullOrEmpty(DateFormat) ? "yyyy-MM-dd" : DateFormat),
                    NexusDateTimeInputMode.Time => dateTime.Value.ToString(ShowMilliseconds ? "HH:mm:ss.fff" : ShowSeconds ? "HH:mm:ss" : string.IsNullOrEmpty(TimeFormat) ? "HH:mm" : TimeFormat),
                    NexusDateTimeInputMode.DateTime => dateTime.Value.ToString(string.IsNullOrEmpty(DateTimeFormat) ? "yyyy-MM-ddTHH:mm" : DateTimeFormat),
                    NexusDateTimeInputMode.Month => dateTime.Value.ToString("yyyy-MM"),
                    NexusDateTimeInputMode.Week => GetWeekFormat(dateTime.Value),
                    _ => dateTime.Value.ToString("yyyy-MM-dd")
                };
            }
            return string.Empty;
        }

        private string DetermineType()
            => Mode switch
            {
                NexusDateTimeInputMode.Date => "date",
                NexusDateTimeInputMode.Time => "time",
                NexusDateTimeInputMode.DateTime => "datetime-local",
                NexusDateTimeInputMode.Month => "month",
                NexusDateTimeInputMode.Week => "week",
                _ => "date"
            };

        private DateTime? ConvertToDateTime(TValue value)
        {
            if (value == null) return null;

            return value switch
            {
                DateTime dateTime => dateTime,
                DateTimeOffset dateTimeOffset => dateTimeOffset.DateTime,
                DateOnly dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
                TimeOnly timeOnly => DateTime.Today.Add(timeOnly.ToTimeSpan()),
                string str => DateTime.TryParse(str, out var dt) ? dt : null,
                _ => null
            };
        }

        private string GetWeekFormat(DateTime dateTime)
        {
            var year = dateTime.Year;
            var week = GetWeekOfYear(dateTime);
            return $"{year}-W{week:00}";
        }

        private int GetWeekOfYear(DateTime dateTime)
        {
            var culture = CultureInfo.CurrentCulture;
            var calendar = culture.Calendar;
            return calendar.GetWeekOfYear(dateTime, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
        }

        private void HandleInput(ChangeEventArgs e)
        {
            var inputValue = e.Value?.ToString();
            if (Mode == NexusDateTimeInputMode.DateTime || Mode == NexusDateTimeInputMode.Date)
            {
                if (DateTime.TryParse(e.Value?.ToString(), out var dateValue))
                    CurrentValue = ConvertToTValue(dateValue.Date);
                else
                    CurrentValue = default!;
            }
            else if (Mode == NexusDateTimeInputMode.Time)
            {
                if (!string.IsNullOrEmpty(inputValue) && TimeSpan.TryParse(inputValue, out var timeValue))
                {
                    var dateTime = DateTime.Today.Add(timeValue);
                    CurrentValue = ConvertToTValue(dateTime);
                }
                else
                    CurrentValue = default!;
            }
            else if (Mode == NexusDateTimeInputMode.Month)
            {
                if (!string.IsNullOrEmpty(inputValue) && inputValue.Contains("-"))
                {
                    var parts = inputValue.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[0], out var year) && int.TryParse(parts[1], out var month))
                    {
                        var date = new DateTime(year, month, 1);
                        CurrentValue = ConvertToTValue(date);
                    }
                }
            }
            else if (Mode == NexusDateTimeInputMode.Week)
            {
                if (!string.IsNullOrEmpty(inputValue) && inputValue.Contains("-W"))
                {
                    var parts = inputValue.Split('-');
                    if (parts.Length >= 2 && int.TryParse(parts[0], out var year))
                    {
                        var weekPart = parts[1].Substring(1);
                        if (int.TryParse(weekPart, out var week))
                        {
                            var date = FirstDateOfWeek(year, week);
                            CurrentValue = ConvertToTValue(date);
                        }
                    }
                }
            }
        }

        private DateTime FirstDateOfWeek(int year, int week)
        {
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            var firstWeekDay = jan1.AddDays(daysOffset);
            var firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

            if (firstWeek <= 1)
                week -= 1;

            return firstWeekDay.AddDays(week * 7);
        }

        private TValue ConvertToTValue(DateTime? dateTime)
        {
            if (!dateTime.HasValue) return default!;

            var type = typeof(TValue);
            var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

            if (underlyingType == typeof(DateTime))
                return (TValue)(object)dateTime.Value;
            else if (underlyingType == typeof(DateTimeOffset))
                return (TValue)(object)new DateTimeOffset(dateTime.Value);
            else if (underlyingType == typeof(DateOnly))
                return (TValue)(object)DateOnly.FromDateTime(dateTime.Value);
            else if (underlyingType == typeof(TimeOnly))
                return (TValue)(object)TimeOnly.FromDateTime(dateTime.Value);
            else if (underlyingType == typeof(string))
                return (TValue)(object)dateTime.Value.ToString();

            return default!;
        }
    }
}
