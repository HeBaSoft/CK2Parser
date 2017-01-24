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

        private static readonly Regex _parser = new Regex(@"^\s*{\n", RegexOptions.Multiline);

        public ValueHolder Read(CachedLineReader reader) {

            if(!_parser.IsMatch(reader.ReadLine()))
                return null;

            StringBuilder builder = new StringBuilder();

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
                this, "wrapper", builder.ToString(), false
            );
        }

        public string Write(ValueHolder source) {
            throw new NotImplementedException();
        }

    }

}
