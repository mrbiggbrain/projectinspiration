using ProjectInspiration.Library.Dice.Exceptions;
using ProjectInspiration.Library.Dice.Models;
using ProjectInspiration.Library.Dice.Models.Request;
using ProjectInspiration.Library.Dice.Models.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectInspiration.Library.Dice
{
    /// <summary>
    /// 
    /// </summary>
    public static class RollProcessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static public RollResult Roll(RollRequest request)
        {
            // Checks and Throws
            if(request == null)
                throw new ArgumentNullException("Request can not be null.");
            if (request.RollText == null)
                throw new ArgumentException($"The request contained empty RollText.");

            List<IResultSet> sets = new List<IResultSet>();

            // Remove whitepace
            String compactedRollText = request.RollText.Replace(" ", "");
            var map = GetMappingTable(compactedRollText);

            foreach(var m in map)
            {
                sets.Add(ProcessSection(m.text, m.mod));
            }

            return new RollResult(sets);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        static private IResultSet ProcessSection(string section, int mod)
        { 
            if (int.TryParse(section, out int result))
            {
                return GenerateStaticSet(result * mod);
            }
            else
            {

                int sepPos = section.ToLower().IndexOf('d');
                if (sepPos > -1)
                {

                    if (int.TryParse(section.Substring(0, sepPos), out int count))
                    {
                        if (int.TryParse(section.Substring(sepPos + 1), out int sides))
                        {
                            return GenerateDiceRollSet(sides, count, mod);
                        }
                        else
                        {
                            throw new RollParseException($"Invalid roll section: {section} (Missing Sides)");
                        }
                    }
                    else
                    {
                        throw new RollParseException($"Invalid roll section: {section} (Missing Count)");
                    }  
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        static private StaticSet GenerateStaticSet(int result)
        {
            return new StaticSet(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sides"></param>
        /// <param name="count"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        static private DiceRollSet GenerateDiceRollSet(int sides, int count, int mod)
        {
            List<DiceRoll> rolls = new List<DiceRoll>();

            foreach (var i in Enumerable.Range(0, count))
            {
                rolls.Add(GenerateDiceRoll(sides, mod));
            }

            return new DiceRollSet(rolls);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sides"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        static private DiceRoll GenerateDiceRoll(int sides, int mod)
        {
            int result = (new Random()).Next(1, sides + 1);
            return new DiceRoll(sides, result * mod);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static private List<(String text, int mod)> GetMappingTable(string text)
        {
            var table = new List<(string text, int mod)>();

            // Get the mods
            List<int> mods = GetMods(text);

            // Get the strings.
            String[] strs = text.Split("+-".ToCharArray());

            if(mods.Count == strs.Count())
            {
                for(int i = 0; i < strs.Count(); i++)
                {
                    table.Add((strs.ElementAt(i), mods.ElementAt(i)));
                }

                return table;
            }
            else
            {
                throw new ArgumentException();
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static private List<int> GetMods(String text)
        {
            var positions = GetSepPositions(text);
            var chars = ExtractChars(text, positions);
            var _mods = ConvertCharListToModList(chars);

            List<int> mods = new List<int>();
            mods.Add(1);
            mods.AddRange(_mods);

            return mods;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static private List<int> GetSepPositions(string text)
        {
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += 1)
            {
                index = text.IndexOfAny("+-".ToCharArray(), index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        static private List<char> ExtractChars(string text, List<int> pos)
        {
            var chars = new List<char>();

            foreach(int p in pos)
            {
                chars.Add(text.ElementAt(p));
            }

            return chars;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        static private List<int> ConvertCharListToModList(List<char> chars)
        {
            var mods = new List<int>();

            foreach(char c in chars)
            {
                mods.Add(ConvertCharToMod(c));
            }

            return mods;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        static private int ConvertCharToMod(char c)
        {
            if (c == '+') return 1;
            else return -1;
        }
    }
}
