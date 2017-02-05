using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CK2Parser.IO;

namespace CK2Parser.Parse {

    internal abstract class ValueParser : IParser {

        // IParser

        public abstract KeyValuePair<string, object> Deserialize(CachedLineReader reader, int nestLevel);

        public abstract bool Serialize(KeyValuePair<string, object> source, StringBuilder builder, int nestLevel);

        // Utilities

        protected static void ResolveNesting(CachedLineReader reader, StringBuilder builder) {
            int nestLevel = 1;

            while(nestLevel > 0) {
                string line = reader.ReadLine();

                nestLevel += line.Count(c => c.Equals('{'));
                nestLevel -= line.Count(c => c.Equals('}'));

                if(nestLevel > 0)
                    builder.Append(line);
            }
        }

        protected static void AppendTabs(StringBuilder builder, int nestLevel) {
            builder.Append(new string('\t', nestLevel));
        }

    }

}
