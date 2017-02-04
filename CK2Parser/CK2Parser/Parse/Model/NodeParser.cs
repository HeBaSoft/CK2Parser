using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CK2Parser.IO;
using CK2Parser.Node;

namespace CK2Parser.Parse.Model {

    internal class NodeParser : IParser {

        private static readonly Regex _parser = new Regex(@"(\w+)=(?:\n.*|){");

        public KeyValuePair<string, object> Deserialize(CachedLineReader reader, int nestLevel) {
            StringBuilder builder = new StringBuilder(
                reader.ReadLine()
            );

            nestLevel += builder.ToString().Count(ch => ch == '\t');

            if(!_parser.IsMatch(builder.ToString())) {
                builder.Append(reader.ReadLine());

                if(!_parser.IsMatch(builder.ToString()))
                    return default(KeyValuePair<string, object>);
            }

            string key = _parser.Match(builder.ToString()).Groups[1].ToString();
            
            builder.Clear();
            Utils.ResolveNesting(reader, builder);

            return new KeyValuePair<string, object>(
                key,
                new Node.Node(builder.ToString(), nestLevel)
            );
        }

        public bool Serialize(KeyValuePair<string, object> source, StringBuilder builder, int nestLevel) {
            if(source.Value is Node.Node) {
                string padding = new string('\t', nestLevel);

                builder.Append(padding); builder.Append(source.Key); builder.AppendLine("=");
                builder.Append(padding); builder.AppendLine("{");
                builder.Append(new NodeSerializer(source.Value as Node.Node).Serialize());
                builder.Append(padding); builder.AppendLine("}");
                return true;
            }

            return false;
        }

    }

}
