using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectInspirationLibrary.Dice
{
    public sealed class RollSaver
    {
        private static readonly RollSaver instance = new RollSaver();

        private Dictionary<String,List<List<RollResult>>> prevRolls = new Dictionary<string, List<List<RollResult>>>();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static RollSaver()
        {
        }

        private RollSaver()
        {
        }

        public static RollSaver Instance
        {
            get
            {
                return instance;
            }
        }

        public void Save(String name, List<List<RollResult>> rolls)
        {
            this.prevRolls[name] = rolls;
        }

        public List<List<RollResult>> Load(String name)
        {
            List<List<RollResult>> results = null;

            this.prevRolls.TryGetValue(name, out results);

            return results;
        }
    }
}
