using System;

namespace ComboTracker
{
    public class ConsoleAnnouncer
    {
        public int Id { get; init; }

        public ConsoleAnnouncer(int id)
        {
            Id = id;
        }

        public ConsoleAnnouncer(int id, ComboTracker tracker)
        {
            Id = id;
            tracker.StreakChanged += OnStreakChanged;
            tracker.MilestoneReached += OnMilestoneReached;
        }

        public void OnStreakChanged(ComboTracker sender, int streak)
        {
            Console.WriteLine($"Комбо: {streak}");
        }

        public void OnMilestoneReached(object? sender, int milestone)
        {
            Console.WriteLine($"Рубеж комбо: {milestone}! Держи темп!");
        }

        public void SubscribeToStreakChanged(ComboTracker tracker)
        {
            tracker.StreakChanged += OnStreakChanged;
        }

        public void UnsubscribeFromStreakChanged(ComboTracker tracker)
        {
            tracker.StreakChanged -= OnStreakChanged;
        }

        public void SubscribeToMilestoneReached(ComboTracker tracker)
        {
            tracker.MilestoneReached += OnMilestoneReached;
        }

        public void UnsubscribeFromMilestoneReached(ComboTracker tracker)
        {
            tracker.MilestoneReached -= OnMilestoneReached;
        }
    }
}