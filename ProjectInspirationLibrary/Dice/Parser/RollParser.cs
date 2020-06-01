using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectInspirationLibrary.Dice.Parser
{

    public enum FilterType { KEEP_HIGH, KEEP_LOW, ADVANTAGE, DISADVANTAGE, NONE}

    public static class RollParser
    {
        public static RollBuilder Parse(String str)
        {

            String choppedRollText = str.Replace(" ", String.Empty).ToLower(); ;

            List<int> signTable = RollParser.GetSignTable(choppedRollText);
            List<String> rollTable = RollParser.GetRollTextTable(choppedRollText);

            RollBuilder builder = new RollBuilder();

            for(int i = 0; i < rollTable.Count; i++)
            {

                (String nakedRollText, FilterType filterType, int filterValue) = PaseFilter(rollTable[i]);

                (int count, int sides) = ParseRollText(nakedRollText);

                int KeepValue = count;

                if(filterType == FilterType.ADVANTAGE)
                {
                    filterType = FilterType.KEEP_HIGH;
                    filterValue = count;
                    count *= 2;
                }
                else if(filterType == FilterType.DISADVANTAGE)
                {
                    filterType = FilterType.KEEP_LOW;
                    filterValue = count;
                    count *= 2;
                }
                else if(filterType == FilterType.NONE)
                {
                    filterType = FilterType.KEEP_HIGH;
                    filterValue = count;
                }

                RollRequest r = builder.AddRequest(signTable[i], count, sides, filterType, filterValue);

                if(signTable[i] == -1)
                {
                    r.IsNeg();
                }
            }

            return builder;
            
        }

        private static (int count, int sides) ParseRollText(string v)
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

        private static (string nakedRollText, FilterType filterType, int filterValue) PaseFilter(string v)
        {
            if (v.Contains("kh"))
            {
                throw new NotImplementedException();
            }
            else if (v.Contains("kl"))
            {
                throw new NotImplementedException();
            }
            else if (v.Contains("adv"))
            {
                var parts = v.Split("adv");

                String text = parts.ElementAt(0);

                return (text, FilterType.ADVANTAGE, 0);
            }
            else if(v.Contains("dis"))
            {
                var parts = v.Split("dis");

                String text = parts.ElementAt(0);

                return (text, FilterType.DISADVANTAGE, 0);
            }
            else
            {
                return (v, FilterType.NONE, 0);
            }
        }

        private static List<string> GetRollTextTable(string str)
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
