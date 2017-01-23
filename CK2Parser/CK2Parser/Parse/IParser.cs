using CK2Parser.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Parser.Parse {

    internal interface IParser {

        KeyValuePair<string, object> Read(CachedLineReader reader);

        string Write(KeyValuePair<string, object> source);

    }

}
