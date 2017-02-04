using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CK2Parser.IO;
using CK2Parser.Node;

namespace CK2Parser.Parse.Model {

    class ArrayParser : IParser {

        private static readonly Regex _parserOneline = new Regex(@"(\w+)=(?:{)(.+)}");
        private static readonly Regex _parserUgly    = new Regex(@"(\w+)=(?:\s+{)(\s.+)}");

        public KeyValuePair<string, object> Deserialize(CachedLineReader reader, int nestLevel) {
            StringBuilder builder = new StringBuilder(
                reader.ReadLine()
            );

            Match match;

            if(_parserOneline.IsMatch(builder.ToString())) {
                match = _parserOneline.Match(builder.ToString());

            } else {
                builder.Append(reader.ReadLine());
                builder.Append(reader.ReadLine());

                if(!_parserUgly.IsMatch(builder.ToString()))
                    return default(KeyValuePair<string, object>);

                match = _parserUgly.Match(builder.ToString());
            }

            return new KeyValuePair<string, object>(
                match.Groups[1].ToString(),
                match.Groups[2].ToString().Trim().Split(' ')
            );
        }

        public bool Serialize(KeyValuePair<string, object> source, StringBuilder builder, int nestLevel) {
            //throw new NotImplementedException();
            return false;
        }

    }

}
