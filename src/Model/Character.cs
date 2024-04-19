using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace rpgAPI.Model
{
    [ExcludeFromCodeCoverage]
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; } = "Frodo";

        public int HitPoint { get; set; } = 10;

        public int Strength{ get; set;} = 10;

        public int Defense { get; set; } =  10;

        public int Intelligence { get; set; }  = 100;

        public RPGClass CharacterClass { get; set; }  = RPGClass.Knight;
        
    }
}