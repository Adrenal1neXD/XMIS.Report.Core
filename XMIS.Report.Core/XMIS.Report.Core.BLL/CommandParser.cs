using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XMIS.Report.Core.BLL
{
    public class CommandParser
    {
        public static void GetPriorityQueue(string src, List<string> res)
        {
            if (res == null)
                res = new List<string>();

            string pattern = @"\s*\w+?(\(|\[)\s*\w+\s*,?\s*\w*\s*(\)|\])";

            var regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(src);

            if (matches.Count == 0)
                return;

            foreach (Match match in matches)
            {
                res.Add(match.ToString());
                src = ReplaceSubstring(src, match.ToString(), "store" + (res.Count - 1));
            }

            GetPriorityQueue(src, res);
        }

        public static KeyValuePair<ActionName, string[]> Parse(string src)
        {
            string tmp = src;
            var tokens = tmp.Split('(', '[', ']', ')');
            if (tokens != null)
                tokens.ToList().RemoveAt(tokens.Length - 1);
            return Normalize(new KeyValuePair<string, string[]>(tokens[0], tokens[1].Split(',')));
        }

        private static KeyValuePair<ActionName, string[]> Normalize(KeyValuePair<string, string[]> src)
        {
            //result have an action name or params?
            ActionName action = ActionName.None;
            object value = null;

            if (src.Key != null || src.Value != null)
            {
                value = src.Value;
                if ((action = CheckActionType(src.Key)) == ActionName.None)
                {
                    action = ActionName.Select;
                    value = new string[] { src.Key, src.Value[0] };
                }
                else
                    value = src.Value;
            }

            return new KeyValuePair<ActionName,string[]>(action, (string[])value);
        }

        private static ActionName CheckActionType(string param)
        {
            var names = typeof(ActionName).GetEnumNames();
            for (int i = 0; i < names.Length; i++)
                if (names[i].ToLower() == param.ToLower())
                    return (ActionName)i;

            return ActionName.None;
        }

        private static string ReplaceSubstring(string source, string replace, string value)
        {
            int index = source.IndexOf(replace);
            return (index < 0)
                ? source
                : source.Replace(replace, value);
        }
    }
}
