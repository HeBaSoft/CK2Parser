using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CK2Parser.IO;
using CK2Parser.Node;

namespace CK2Parser.Parse.Model {

    internal class NodeParser : ValueParser {

        private static readonly Regex _parser = new Regex(@"(\w+)=(?:\n.*|){");

        public override KeyValuePair<string, object> Deserialize(CachedLineReader reader, int nestLevel) {
            StringBuilder builder = new StringBuilder(
                reader.ReadLine()
            );

            if(!_parser.IsMatch(builder.ToString())) {
                builder.Append(reader.ReadLine());

                if(!_parser.IsMatch(builder.ToString()))
                    return default(KeyValuePair<string, object>);
            }

            string key = _parser.Match(builder.ToString()).Groups[1].ToString();
            
            builder.Clear();
            ResolveNesting(reader, builder);

            return new KeyValuePair<string, object>(
                key,
                new Node.Node(builder.ToString(), nestLevel + 1)
            );
        }

        public override bool Serialize(KeyValuePair<string, object> source, StringBuilder builder, int nestLevel) {
            if(!(source.Value is Node.Node))
                return false;

            AppendTabs(builder, nestLevel);
            builder.Append(source.Key);
            builder.AppendLine("=");

            AppendTabs(builder, nestLevel);
            builder.AppendLine("{");

            builder.Append(new NodeSerializer(source.Value as Node.Node).Serialize());

            AppendTabs(builder, nestLevel);
            builder.AppendLine("}");
            return true;
        }

    }

}
