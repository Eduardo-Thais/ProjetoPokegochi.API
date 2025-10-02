using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokegochi
{
    internal class Mascote
    {
        public required string name
        {
            get; set;
        }
        public double height
        {
            get; set;
        }
        public double weight
        {
            get; set;
        }
        public required List<AbilityWrapper> abilities
        {
            get; set;
        }
    }

    internal class AbilityWrapper
    {
        public required Ability ability
        {
            get; set;
        }
    }

    internal class Ability
    {
        public required string name
        {
            get; set;
        }
        public required string url
        {
            get; set;
        }
    }
}
