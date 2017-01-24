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

        public ValueHolder Read(CachedLineReader reader) {
            string raw = reader.ReadLine();

            if(!_parser.IsMatch(raw))
                return null;

            Match match = _parser.Match(raw);
            object value = match.Groups[2].ToString();

            if(value.Equals("yes") || value.Equals("no")) {
                value = value.Equals("yes");
            } else {
                value = (value as string).Replace("\"", string.Empty);
            }

            return new ValueHolder(
                this, match.Groups[1].ToString(), value
            );
        }

        public string Write(ValueHolder source) {
            throw new NotImplementedException();
        }

    }

}
