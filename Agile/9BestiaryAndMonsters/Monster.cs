using System;
using System.Collections.Generic;
using System.Linq;

namespace BestiarySystem
{
    public class Monster
    {
        public string Id { get; }
        public string Name { get; }
        public ThreatLevel Threat { get; }
        
        private readonly List<Ability> _abilities = new List<Ability>();
        
        public IReadOnlyList<Ability> Abilities => _abilities;

        public Monster(string id, string name, ThreatLevel threat)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException(
                    "ID не может быть пустым",
                    nameof(id));
            
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    "Имя не может быть пустым",
                    nameof(name));

            Id = id;
            Name = name;
            Threat = threat;
        }

        public void AddAbility(Ability ability)
        {
            if (ability == null)
                throw new ArgumentNullException(nameof(ability));
            
            _abilities.Add(ability);
        }

        public void AddAbilities(params Ability[] abilities)
        {
            if (abilities == null)
                throw new ArgumentNullException(nameof(abilities));
            
            _abilities.AddRange(abilities.Where(a => a != null));
        }

        public override string ToString()
        {
            return $"{Name} (ID: {Id}, Угроза: {Threat}, Способностей: {_abilities.Count})";
        }
    }
}