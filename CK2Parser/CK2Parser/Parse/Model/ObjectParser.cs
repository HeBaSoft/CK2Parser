using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CK2Parser.IO;
using CK2Parser.Node;

namespace CK2Parser.Parse.Model {

    internal class ObjectParser : IParser {

        private static readonly Regex _parser = new Regex(@"(\w+)=(?:\n.*|){");

        public KeyValuePair<string, object> Read(CachedLineReader reader) {
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

            // Resolve nesting
            int nestLevel = 1;
            while(nestLevel > 0) {
                string line = reader.ReadLine();
                nestLevel += line.Count(c => c.Equals('{'));
                nestLevel -= line.Count(c => c.Equals('}'));

                if(nestLevel > 0)
                    builder.Append(line);
            }

            return new KeyValuePair<string, object> (
                key,
                new NodeResolver(builder.ToString()).Resolve() // TODO: Make this resolve during retrieving value
            );
        }

        public string Write(KeyValuePair<string, object> source) {
            throw new NotImplementedException();
        }

    }

}
