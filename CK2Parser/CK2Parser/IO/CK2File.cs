using CK2Parser.Node;
using CK2Parser.Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Parser.IO {

    public class CK2File {

        public static readonly string FormatKey = "CK2txt";

        private FileInfo _file;

        public CK2File(string path) {
            _file = new FileInfo(path);
        }

        public dynamic Read() {
            if(!_file.Exists)
                throw new Exception(string.Format("File {0} does not exist", _file.FullName));

            CachedLineReader reader = new CachedLineReader(_file.OpenText());

            // Verify format
            if(!reader.ReadLine().Contains(FormatKey))
                throw new Exception("File is not valid .ck2 format");

            return new NodeResolver(reader).Resolve();
        }

        public void Write(dynamic source) {
            throw new NotImplementedException();
        }

    }

}
