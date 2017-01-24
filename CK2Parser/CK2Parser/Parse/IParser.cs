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

        ValueHolder Read(CachedLineReader reader);

        string Write(ValueHolder source);

    }

}
