using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CK2Parser.IO;
using System.Text.RegularExpressions;
using CK2Parser.Node;

namespace CK2Parser.Parse.Model {

    class WrapperParser : IParser {

        public static readonly string Key = "wrapper";

        private static readonly Regex _parser = new Regex(@"^\s*{\n", RegexOptions.Multiline);

        public KeyValuePair<string, object> Deserialize(CachedLineReader reader, int nestLevel) {
            if(!_parser.IsMatch(reader.ReadLine()))
                return default(KeyValuePair<string, object>);

            StringBuilder builder = new StringBuilder();
            Utils.ResolveNesting(reader, builder);

            return new KeyValuePair<string, object>(
                Key,
                new Node.Node(builder.ToString(), nestLevel + 1)
            );
        }

        public bool Serialize(KeyValuePair<string, object> source, StringBuilder builder, int nestLevel) {
            if(source.Key != Key)
                return false;

            string padding = new string('\t', nestLevel);

            builder.Append(padding); builder.AppendLine("{");
            builder.Append(new NodeSerializer(source.Value as Node.Node).Serialize());
            builder.Append(padding); builder.AppendLine("}");
            return true;
        }

    }

}
