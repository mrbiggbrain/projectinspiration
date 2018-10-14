using ProjectInspiration.Library.Dice.Models;
using ProjectInspiration.Library.Dice.Models.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscordBot.Formatters
{
    static class RollResponceFormatter
    {
        public static String Format(IRollResult responce)
        {
            String str = "";

            foreach(IResultSet set in responce.Sets)
            {
                // Add either a + or - if this is not the first set. 
                if(!String.IsNullOrEmpty(str))
                {
                    if (set.Value > 0) str += " + ";
                    else str += " - ";
                }

                str += FormatSet(set);
            }

            return str;
        }

        private static String FormatSet(IResultSet set)
        {
            if (set.Rolls.Count > 1)
            {
                String SetString = String.Join(", ", set.Rolls.Select(r => FormatRoll(r)));
                return $"{Math.Abs(set.Value)} ( {SetString} )";
            }
            else if (set.Rolls.Count == 1)
            {
                return $"{FormatRoll(set.Rolls.First())}";
            }
            else return "?";
            
        }

        private static String FormatRoll(IRoll roll)
        {
            bool isCrit = false;

            if (roll.Value == 1 || roll.Value == roll.Sides) isCrit = true;

            return DiscordFormatter.Bold($"{Math.Abs(roll.Value)}", isCrit);
        }
    }
}
