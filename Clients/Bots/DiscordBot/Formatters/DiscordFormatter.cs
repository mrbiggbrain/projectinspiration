using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Formatters
{
    public static class DiscordFormatter
    {
        public static String Format(String text, bool bold = false, bool underline = false, bool striked = false, bool italics = false, bool code = false)
        {
            return Bold(
                Underline(
                    Striked(
                        Italics(
                            Code(
                                text, code), italics), striked), underline), bold);
        }

        public static String Bold(String text, bool isBold = true)
        {
            if (isBold) return $"**{text}**";
            else return text;
        }

        public static String Underline(String text, bool isUnderlined = true)
        {
            if (isUnderlined) return $"__{text}__";
            else return text;
        }

        public static String Striked(String text, bool isStriked = true)
        {
            if (isStriked) return $"~~{text}~~";
            else return text;
        }

        public static String Italics(String text, bool isItalicized = true)
        {
            if (isItalicized) return $"*{text}*";
            else return text;
        }

        public static String Code(String text, bool isCode = true)
        {
            if (isCode) return $"`{text}`";
            else return text;
        }
    }
}
