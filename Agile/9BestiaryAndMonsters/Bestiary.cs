using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BestiarySystem
{
    public class Bestiary : IEnumerable<Monster>
    {
        private readonly List<Monster> _monsters = new List<Monster>();
        private readonly Dictionary<string, Monster> _byId
            = new Dictionary<string, Monster>();
        
        public int Count => _monsters.Count;

        public Monster this[int index]
        {
            get
            {
                if (index < 0 || index >= _monsters.Count)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(index), 
                        $"Индекс {index} вне диапазона [0, {_monsters.Count - 1}]"
                    );
                }
                
                return _monsters[index];
            }
        }

        public Monster this[string id]
        {
            get
            {
                if (id is null)
                {
                    throw new ArgumentNullException(nameof(id));
                }
                
                if (!_byId.TryGetValue(id, out Monster? monster) || monster is null)
                {
                    throw new KeyNotFoundException($"Монстр с ID '{id}' не найден");
                }
                
                return monster;
            }
        }

        public void Add(Monster monster)
        {
            if (monster is null)
            {
                throw new ArgumentNullException(nameof(monster));
            }
            
            if (_byId.ContainsKey(monster.Id))
            {
                throw new ArgumentException(
                    $"Монстр с ID '{monster.Id}' уже существует", 
                    nameof(monster)
                );
            }

            _monsters.Add(monster);
            _byId.Add(monster.Id, monster);
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= _monsters.Count)
            {
                return false;
            }

            var monster = _monsters[index];
            _monsters.RemoveAt(index);
            return _byId.Remove(monster.Id);
        }

        public bool RemoveById(string id)
        {
            if (id is null)
            {
                return false;
            }

            if (!_byId.TryGetValue(id, out Monster? monster) || monster is null)
            {
                return false;
            }

            _monsters.Remove(monster);
            return _byId.Remove(id);
        }

        public IEnumerable<Monster> EnumerateByThreat(ThreatLevel minThreat)
        {
            foreach (var monster in _monsters)
            {
                if (monster.Threat >= minThreat)
                {
                    yield return monster;
                }
            }
        }

        public IEnumerator<Monster> GetEnumerator()
        {
            return _monsters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsSynchronized()
        {
            if (_monsters.Count != _byId.Count)
            {
                return false;
            }

            foreach (var monster in _monsters)
            {
                if (!_byId.TryGetValue(monster.Id, out var found) || found != monster)
                {
                    return false;
                }
            }

            return true;
        }
    }
}