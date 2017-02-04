using CK2Parser.IO;
using CK2Parser.Node;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CK2Parser.Parse.Model {

    internal class PrimitiveParser : IParser {

        private static readonly Regex _parser = new Regex(@"(\w+)=([^{\n]+)");

        public KeyValuePair<string, object> Deserialize(CachedLineReader reader, int nestLevel) {
            string raw = reader.ReadLine();

            if(!_parser.IsMatch(raw))
                return default(KeyValuePair<string, object>);

            Match match = _parser.Match(raw);
            object value = match.Groups[2].ToString();

            if(value.Equals("yes") || value.Equals("no")) {
                value = value.Equals("yes");
            }

            return new KeyValuePair<string, object>(
                match.Groups[1].ToString(),
                value
            );
        }

        public bool Serialize(KeyValuePair<string, object> source, StringBuilder builder, int nestLevel) {
            object value = source.Value;

            if(source.Key != WrapperParser.Key && (value.GetType().IsPrimitive || value is string)) {

                if(value is bool) {
                    value = Convert.ToBoolean(value) ? "yes" : "no";
                }

                builder.Append(new string('\t', nestLevel));
                builder.Append(source.Key);
                builder.Append("=");
                builder.AppendLine(value.ToString());
                return true;
            }

            return false;
        }

    }

}
