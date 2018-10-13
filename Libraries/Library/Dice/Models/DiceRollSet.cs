using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ProjectInspiration.Library.Dice;

namespace ProjectInspiration.Library.Dice.Models
{
    public class DiceRollSet : IResultSet
    {
        List<DiceRoll> rolls;

        public int Value
        {
            get
            {
                return rolls.Sum(r => r.Value);
            }
        }

        public List<IRoll> Rolls
        {
            get
            {
                return rolls.Cast<IRoll>().ToList();
            }
        }

        public string Display
        {
            get
            {
                if(this.rolls.Count > 1)
                {
                    return $"{this.Value} ({String.Join(", ", this.rolls.Select(x => x.Value.ToString()))})";
                }
                else
                {
                    return this.Value.ToString();
                }
            }
        }

        public DiceRollSet(IEnumerable<DiceRoll> rolls) => this.rolls = rolls.ToList();

    }
}
