using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ProjectInspiration.Library.Dice.Models
{
    public class RollResponce
    {
        private List<IResultSet> sets;

        public List<IResultSet> Sets
        {
            get => sets;
        }

        public RollResponce(List<IResultSet> sets)
        {
            this.sets = sets;
        }

        public int Value
        {
            get
            {
                return sets.Sum(s => s.Value);
            }
        }

        public String Display
        {
            get
            {
                return String.Join(" + ", this.sets.Select(x => x.Display));
            }
        }
    }
}
