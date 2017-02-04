using CK2Parser.IO;
using CK2Parser.Node;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Parser.Parse {

    internal interface IParser {

        KeyValuePair<string, object> Deserialize(CachedLineReader reader, int nestLevel);

        bool Serialize(KeyValuePair<string, object> source, StringBuilder builder, int nestLevel);

    }

}
