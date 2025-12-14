using System;

namespace ComboTracker
{
    public class StreakStats
    {
        public int Id { get; init; }
        private int _resetCount;
        private int _maxStreak;

        public StreakStats(int id)
        {
            Id = id;
            _resetCount = 0;
            _maxStreak = 0;
        }

        public StreakStats(int id, ComboTracker tracker)
        {
            Id = id;
            tracker.StreakChanged += OnStreakChanged;
        }

        public void OnStreakChanged(ComboTracker sender, int streak)
        {
            if (streak == 0)
            {
                _resetCount++;
            }

            if (streak > _maxStreak)
            {
                _maxStreak = streak;
            }
        }

        public void SubscribeToStreakChanged(ComboTracker tracker)
        {
            tracker.StreakChanged += OnStreakChanged;
        }

        public void UnsubscribeFromStreakChanged(ComboTracker tracker)
        {
            tracker.StreakChanged -= OnStreakChanged;
        }

        public void Report()
        {
            Console.WriteLine($"\nСТАТИСТИКА");
            Console.WriteLine($"Сбросов: {_resetCount}");
            Console.WriteLine($"Максимальное комбо: {_maxStreak}");
        }
    }
}