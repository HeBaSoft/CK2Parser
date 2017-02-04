using CK2Parser.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Parser.Parse {

    internal static class Utils {

        public static void ResolveNesting(CachedLineReader reader, StringBuilder builder) {
            int nestLevel = 1;

            while(nestLevel > 0) {
                string line = reader.ReadLine();

                nestLevel += line.Count(c => c.Equals('{'));
                nestLevel -= line.Count(c => c.Equals('}'));

                if(nestLevel > 0)
                    builder.Append(line);
            }
        }

    }

}
