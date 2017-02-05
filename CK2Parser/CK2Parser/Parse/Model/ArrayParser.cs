using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CK2Parser.IO;
using CK2Parser.Node;

namespace CK2Parser.Parse.Model {

    class ArrayParser : ValueParser {

        private static readonly Regex _parserOneline    = new Regex(@"(\w+)=(?:{)(.+)}");
        private static readonly Regex _parserMultiline  = new Regex(@"(\w+)=(?:\s+{)(\s.+)}");

        public override KeyValuePair<string, object> Deserialize(CachedLineReader reader, int nestLevel) {
            StringBuilder builder = new StringBuilder(
                reader.ReadLine()
            );

            Match match;

            if(_parserOneline.IsMatch(builder.ToString())) {
                match = _parserOneline.Match(builder.ToString());

            } else {
                builder.Append(reader.ReadLine());
                builder.Append(reader.ReadLine());

                if(!_parserMultiline.IsMatch(builder.ToString()))
                    return default(KeyValuePair<string, object>);

                match = _parserMultiline.Match(builder.ToString());
            }

            return new KeyValuePair<string, object>(
                match.Groups[1].ToString(),
                match.Groups[2].ToString().Trim().Split(' ')
            );
        }

        public override bool Serialize(KeyValuePair<string, object> source, StringBuilder builder, int nestLevel) {
            if(!source.Value.GetType().IsArray)
                return false;

            AppendTabs(builder, nestLevel);
            builder.Append(source.Key);
            builder.Append("={");
            builder.Append(string.Join(" ", source.Value as object[]));
            builder.AppendLine("}");
            return true;
        }

    }

}
