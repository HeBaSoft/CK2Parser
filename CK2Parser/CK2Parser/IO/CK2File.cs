using CK2Parser.Node;
using CK2Parser.Parse;
using CK2Parser.Parse.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Parser.IO {

    public partial class CK2File {

        private FileInfo    _file;
        private Node.Node   _node;

        public dynamic Structure {
            get { return _node; }
        }

        internal CK2File(FileInfo file, Node.Node node) {
            _file = file;
            _node = node;
        }

        // TODO: Search

    }

}
