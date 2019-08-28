using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectInspirationLibrary.Dice.Parser
{
    public static class RollParser
    {
        public static RollBuilder Parse(String str)
        {

            String choppedRollText = str.Replace(" ", String.Empty);

            List<int> signTable = RollParser.GetSignTable(choppedRollText);
            List<String> rollTable = RollParser.GetRolTextlTable(choppedRollText);

            RollBuilder builder = new RollBuilder();

            for(int i = 0; i < rollTable.Count; i++)
            {
                (int count, int sides) = ParseRolltext(rollTable[i]);

                RollRequest r = builder.AddRequest(signTable[i], count, sides);

                if(signTable[i] == -1)
                {
                    r.IsNeg();
                }
            }

            return builder;
            
        }

        private static (int count, int sides) ParseRolltext(string v)
        {
            String[] table = v.Split('d');

            int count = 0;
            int sides = 20;
            
            // Parse count
            if(table.Length > 0)
            {
                if(Int32.TryParse(table[0], out count))
                {
                    // parsed;
                }

                // Parse sides
                if (table.Length > 1)
                {
                    if (Int32.TryParse(table[1], out sides))
                    {
                        // parsed;
                    }
                }
                else
                {
                    sides = count;
                    count = 0;
                }
            }

            

            return (count, sides);
        }

        private static List<string> GetRolTextlTable(string str)
        {
            return str.Split(new char[] { '+', '-' }).ToList();
        }

        private static List<int> GetSignTable(string str)
        {
            List<int> results = new List<int>();
            results.Add(1);

            for(int i = 0; i < str.Length; i++)
            {
                if (str[i] == '+') results.Add(1);
                else if (str[i] == '-') results.Add(-1);
            }

            return results;
        }
    }
}
