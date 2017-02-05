using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CK2Parser.IO;
using System.Text.RegularExpressions;
using CK2Parser.Node;

namespace CK2Parser.Parse.Model {

    class WrapperParser : ValueParser {

        private static readonly Regex _parser = new Regex(@"^\s*{\n", RegexOptions.Multiline);

        public static readonly string Key = "wrapper";

        public override KeyValuePair<string, object> Deserialize(CachedLineReader reader, int nestLevel) {
            if(!_parser.IsMatch(reader.ReadLine()))
                return default(KeyValuePair<string, object>);

            StringBuilder builder = new StringBuilder();
            ResolveNesting(reader, builder);

            return new KeyValuePair<string, object>(
                Key,
                new Node.Node(builder.ToString(), nestLevel + 1)
            );
        }

        public override bool Serialize(KeyValuePair<string, object> source, StringBuilder builder, int nestLevel) {
            if(source.Key != Key)
                return false;

            AppendTabs(builder, nestLevel);
            builder.AppendLine("{");

            builder.Append(new NodeSerializer(source.Value as Node.Node).Serialize());

            AppendTabs(builder, nestLevel);
            builder.AppendLine("}");
            return true;
        }

    }

}
