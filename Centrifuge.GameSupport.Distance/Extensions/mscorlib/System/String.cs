using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public static class System__StringExtensions
{
    const string WordWrapRegexPattern = "(?<tag>[<][/]?[a-zA-Z0-9=\\\"#]+[>])";

    public static string WordWrap(this string text, int lineLength)
    {
        int position;
        int next;
        var sb = new StringBuilder();

        if (lineLength < 1)
        {
            return text;
        }

        for (position = 0; position < text.Length; position = next)
        {
            int lineEnd = text.IndexOf('\n', position);

            if (lineEnd == -1)
            {
                next = lineEnd = text.Length;
            }
            else
            {
                next = lineEnd + 1;
            }

            if (lineEnd > position)
            {
                do
                {
                    int length = lineEnd - position;

                    if (length > lineLength)
                    {
                        length = LineBreak(text, position, lineLength);
                    }

                    sb.Append(text, position, length);
                    sb.Append('\n');

                    position += length;
                    while (position < lineEnd && char.IsWhiteSpace(text[position]))
                    {
                        position++;
                    }

                }
                while (lineEnd > position);
            }
            else
            {
                sb.Append('\n');
            }
        }
        return sb.ToString();
    }

    // returns a length, not a position
    internal static int LineBreak(string text, int where, int max)
    {
        Regex regex = new Regex(WordWrapRegexPattern);
        Match match;

        int end = text.Substring(where).IndexOf('\n');

        if (end < 0)
        {
            end = text.Substring(where).Length;
        }

        int i; // stores current position (relative)
        int i_sub = 0; // stores the total size of the tags
        int pos; // stores current position (absolute)
        bool valid_found = false;
        int valid = 1; //stores the last valid cutting point
        string with = ""; // the text with the tags
        string without = ""; // the text without the tags
        string temp; // temporary string for holding values
        System.Text.RegularExpressions.Group tag; // a temporary value to store matches from the regex

        for (i = 0; i <= end;)
        {
            if (where + i >= text.Length - 1 || without.Length > max)
            {
                break;
            }

            pos = where + i;
            temp = text.Substring(pos);
            match = regex.Match(temp);

            if (match.Success)
            {
                tag = match.Groups["tag"];
                if (tag != null && temp.StartsWith(tag.Value))
                {
                    temp = tag.Value;
                    i_sub += temp.Length;
                    i += temp.Length;
                    with += temp;
                    valid = i;
                    valid_found = true;
                    continue;
                }
            }

            char chr = text[pos];

            if (char.IsWhiteSpace(chr))
            {
                valid = i + 1;
                valid_found = true;
            }

            with += chr;
            without += chr;
            i++;
        }
        return without.Length < max ? with.Length + 1 : valid_found ? valid : max + i_sub;
    }

    public static Color ToColor(this string hexString)
    {
        var actualColorString = hexString.StartsWith("#") ? hexString.Substring(1, hexString.Length - 1) : hexString;

        if (actualColorString.Length % 2 != 0)
        {
            return Color.black;
        }

        if (actualColorString.Length < 6)
        {
            return Color.black;
        }

        if (actualColorString.Length > 8)
        {
            return Color.black;
        }

        return ParseHex(actualColorString);
    }

    private static Color32 ParseHex(string hexString)
    {
        if (!byte.TryParse(hexString.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out byte r))
        {
            return Color.black;
        }

        if (!byte.TryParse(hexString.Substring(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out byte g))
        {
            return Color.black;
        }

        if (!byte.TryParse(hexString.Substring(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out byte b))
        {
            return Color.black;
        }

        byte a = 255;

        if (hexString.Length == 8 && !byte.TryParse(hexString.Substring(6, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out a))
        {
            return Color.black;
        }

        return new Color32(r, g, b, a);
    }
}