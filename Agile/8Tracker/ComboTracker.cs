using System;

namespace ComboTracker
{
    public delegate void ComboEventHandler(ComboTracker sender, int streak);

    public class ComboTracker
    {
        private const double INCREASE_PROBABILITY = 0.7;          
        private const int MIN_INCREASE_AMOUNT = 1;                
        private const int MAX_INCREASE_AMOUNT_EXCLUSIVE = 4;      
        private const int MIN_STEPS = 12;                         
        private const int MAX_STEPS_EXCLUSIVE = 17;               

        private int _currentStreak;
        private readonly Random _random;
        
        public event ComboEventHandler? StreakChanged;
        
        public event EventHandler<int>? MilestoneReached
        {
            add
            {
                _milestoneReached += value;
                Console.WriteLine("подписчик добавлен");
            }
            remove
            {
                if (_milestoneReached != null)
                {
                    _milestoneReached -= value;
                    Console.WriteLine("подписчик удалён");
                }
            }
        }

        public int CurrentStreak => _currentStreak;

        public ComboTracker()
        {
            _currentStreak = 0;      
            _random = new Random();
        }

        public void Start()
        {
            int steps = _random.Next(MIN_STEPS, MAX_STEPS_EXCLUSIVE);
            Console.WriteLine("Начинаем симуляцию на {0} шагов...\n", steps);

            for (int i = 0; i < steps; i++)
            {
                int oldStreak = _currentStreak;
                
                if (_random.NextDouble() < INCREASE_PROBABILITY)
                {
                    _currentStreak += _random.Next(MIN_INCREASE_AMOUNT, MAX_INCREASE_AMOUNT_EXCLUSIVE);
                    Console.WriteLine("Шаг {0}: Комбо увеличилось до {1}", i + 1, _currentStreak);
                }
                else
                {
                    _currentStreak = 0;  
                    Console.WriteLine("Шаг {0}: Комбо сброшено в 0", i + 1);
                }

                StreakChanged?.Invoke(this, _currentStreak);

                CheckMilestones(oldStreak, _currentStreak);
            }
        }

        private void CheckMilestones(int oldStreak, int newStreak)
        {
            if (oldStreak >= newStreak) return;

            for (int milestone = 10; milestone <= newStreak; milestone += 10)
            {
                if (oldStreak < milestone && newStreak >= milestone)
                {
                    _milestoneReached?.Invoke(this, milestone);
                }
            }
        }

        private EventHandler<int>? _milestoneReached;
    }
}