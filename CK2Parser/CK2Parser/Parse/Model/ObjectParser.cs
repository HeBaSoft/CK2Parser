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

        public ValueHolder Read(CachedLineReader reader) {
            StringBuilder builder = new StringBuilder(
                reader.ReadLine()
            );

            if(!_parser.IsMatch(builder.ToString())) {
                builder.Append(reader.ReadLine());

                if(!_parser.IsMatch(builder.ToString()))
                    return null;
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

            return new ValueHolder(
                this, key, builder.ToString(), false
            );
        }

        public string Write(ValueHolder source) {
            throw new NotImplementedException();
        }

    }

}
