using Microsoft.AspNetCore.Components;
using System.Timers;

namespace NexusUserTest.User.Views
{
    public partial class Countdown : ComponentBase, IDisposable
    {
        [Parameter]
        public EventCallback TimerOut { get; set; }
        [Parameter]
        public EventCallback<int> TimerPause { get; set; }

        private System.Timers.Timer _timer = null!;
        private int _secondsToRun = 0;
        protected string Time { get; set; } = "00:00:00";

        protected string Hours { get; set; } = "00";
        protected string Minutes { get; set; } = "00";
        protected string Seconds { get; set; } = "00";

        protected override void OnInitialized()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
        }

        private async void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            await InvokeAsync(() =>
            {
                _secondsToRun--;
                var timeSpan = TimeSpan.FromSeconds(_secondsToRun);
                //Time = timeSpan.ToString(@"hh\:mm\:ss");

                Hours = timeSpan.Hours.ToString("D2");
                Minutes = timeSpan.Minutes.ToString("D2");
                Seconds = timeSpan.Seconds.ToString("D2");

                StateHasChanged();

                if (_secondsToRun <= 0)
                {
                    _timer.Stop();
                    TimerOut.InvokeAsync();
                }
            });            
        }

        public void Start(int secondsToRun)
        {
            _secondsToRun = secondsToRun;

            if (_secondsToRun > 0)
            {
                var timeSpan = TimeSpan.FromSeconds(_secondsToRun);
                //Time = timeSpan.ToString(@"hh\:mm\:ss");

                Hours = timeSpan.Hours.ToString("D2");
                Minutes = timeSpan.Minutes.ToString("D2");
                Seconds = timeSpan.Seconds.ToString("D2");

                StateHasChanged();
                _timer.Start();
            }
        }

        public async void Pause()
        {
            _timer.Stop();
            await TimerPause.InvokeAsync(_secondsToRun);
        }

        public void Stop() => _timer.Stop();

        public void Dispose() => _timer.Dispose();
    }
}
