using System;

namespace ComboTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Трекер комбо-серии игрока\n");

            var tracker = new ComboTracker();

            var announcer = new ConsoleAnnouncer(1, tracker);
            var stats = new StreakStats(2, tracker);

            Console.WriteLine("ЗАПУСК СИМУЛЯЦИИ");
            
            tracker.Start();

            Console.WriteLine("\nОТПИСКА ОТ СОБЫТИЙ");
            tracker.MilestoneReached -= announcer.OnMilestoneReached;

            stats.Report();

            Console.WriteLine("\nПрограмма завершена!");
        }
    }
}