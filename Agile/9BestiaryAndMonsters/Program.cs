using System;
using System.Linq;

namespace BestiarySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Система Бестиария RPG\n");

            var bestiary = new Bestiary();

            var abilities = new[]
            {
                new Ability("bite", 15, 2),
                new Ability("fire_breath", 40, 10),
                new Ability("poison_spit", 25, 5),
                new Ability("stomp", 30, 3),
                new Ability("heal", -20, 8)
            };

            var dragon = new Monster("dragon_001", "Древний Дракон", ThreatLevel.Mythic);
            dragon.AddAbilities(abilities[0], abilities[1], abilities[3]);

            var spider = new Monster("spider_001", "Ядовитый Паук", ThreatLevel.Severe);
            spider.AddAbilities(abilities[0], abilities[2]);

            var goblin = new Monster("goblin_001", "Гоблин-Разведчик", ThreatLevel.Minor);
            goblin.AddAbility(abilities[0]);

            var troll = new Monster("troll_001", "Горный Тролль", ThreatLevel.Deadly);
            troll.AddAbilities(abilities[3], abilities[4]);

            bestiary.Add(dragon);
            bestiary.Add(spider);
            bestiary.Add(goblin);
            bestiary.Add(troll);

            Console.WriteLine($"Всего монстров: {bestiary.Count}\n");

            Console.WriteLine("=== Доступ по индексу (List) ===");
            for (int i = 0; i < bestiary.Count; i++)
            {
                Console.WriteLine($"[{i}] {bestiary[i]}");
            }

            Console.WriteLine("\n=== Доступ по ID (Dictionary) ===");
            Console.WriteLine($"ID 'dragon_001': {bestiary["dragon_001"]}");
            Console.WriteLine($"ID 'spider_001': {bestiary["spider_001"]}");

            Console.WriteLine("\n=== IReadOnlyList Demo (Способности) ===");
            Console.WriteLine($"Дракон имеет {dragon.Abilities.Count} способност{(dragon.Abilities.Count == 1 ? "ь" : "и")}:");
            foreach (var ability in dragon.Abilities)
            {
                Console.WriteLine($"  - {ability}");
            }

            Console.WriteLine("\n=== Индексаторы с исключениями ===");
            try
            {
                var invalid = bestiary[10];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"ArgumentOutOfRangeException: {ex.Message}");
            }

            try
            {
                var invalid = bestiary["nonexistent"];
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"KeyNotFoundException: {ex.Message}");
            }

            Console.WriteLine("\n=== Yield Return Generator (Угроза ≥ Severe) ===");
            foreach (var monster in bestiary.EnumerateByThreat(ThreatLevel.Severe))
            {
                Console.WriteLine($"- {monster}");
            }

            Console.WriteLine("\n=== Синхронизация при удалении ===");
            Console.WriteLine($"Удаляем гоблина по ID: {bestiary.RemoveById("goblin_001")}");
            Console.WriteLine($"Монстров после удаления: {bestiary.Count}");
            Console.WriteLine($"Структуры синхронизированы: {bestiary.IsSynchronized()}");

            Console.WriteLine("\n=== Поддержка foreach ===");
            foreach (var monster in bestiary)
            {
                Console.WriteLine($"- {monster}");
            }

            Console.WriteLine("\nВсе требования успешно реализованы!");
        }
    }
}