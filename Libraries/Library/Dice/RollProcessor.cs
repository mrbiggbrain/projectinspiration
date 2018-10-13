using ProjectInspiration.Library.Dice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectInspiration.Library.Dice
{
    public static class RollProcessor
    {
        //static public RollResponce Roll(RollRequest request)
        //{
        //    IEnumerable<String> setStrings = request.RollText.Replace(" ", "").Split('+');

        //    List<IResultSet> sets = ProcessSections(setStrings);

        //    return new RollResponce(sets);
        //}

        //static private List<IResultSet> ProcessSections(IEnumerable<String> sections)
        //{
        //    return sections.Select(section => ProcessSection(section)).ToList();
        //}

        static public RollResponce Roll(RollRequest request)
        {
            List<IResultSet> sets = new List<IResultSet>();

            // Remove whitepace
            String compactedRollText = request.RollText.Replace(" ", "");
            var map = GetMappingTable(compactedRollText);

            foreach(var m in map)
            {
                sets.Add(ProcessSection(m.text, m.mod));
            }

            return new RollResponce(sets);
        }

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
                    }

                    throw new ArgumentException();

                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        static private StaticSet GenerateStaticSet(int result)
        {
            return new StaticSet(result);
        }

        static private DiceRollSet GenerateDiceRollSet(int sides, int count, int mod)
        {
            List<DiceRoll> rolls = new List<DiceRoll>();

            foreach (var i in Enumerable.Range(0, count))
            {
                rolls.Add(GenerateDiceRoll(sides, mod));
            }

            return new DiceRollSet(rolls);
        }

        static private DiceRoll GenerateDiceRoll(int sides, int mod)
        {
            int result = (new Random()).Next(1, sides + 1);
            return new DiceRoll(sides, result * mod);
        }

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

        static private List<char> ExtractChars(string text, List<int> pos)
        {
            var chars = new List<char>();

            foreach(int p in pos)
            {
                chars.Add(text.ElementAt(p));
            }

            return chars;
        }

        static private List<int> ConvertCharListToModList(List<char> chars)
        {
            var mods = new List<int>();

            foreach(char c in chars)
            {
                mods.Add(ConvertCharToMod(c));
            }

            return mods;
        }

        static private int ConvertCharToMod(char c)
        {
            if (c == '+') return 1;
            else return -1;
        }
    }
}
