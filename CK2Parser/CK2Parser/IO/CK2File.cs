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

    public class CK2File {

        internal static readonly IParser[] Parsers = new IParser[] {
            new PrimitiveParser(),
            new ArrayParser(),
            new WrapperParser(),
            new NodeParser()
        };

        public static readonly string FormatKey = "CK2txt";

        private FileInfo _file;

        public CK2File(string path) {
            _file = new FileInfo(path);
        }

        public dynamic Read() {
            if(!_file.Exists)
                throw new Exception(string.Format("File {0} does not exist", _file.FullName));

            string content;
            using(StreamReader reader = _file.OpenText())
                content = reader.ReadToEnd();

            // Verify format
            if(!content.StartsWith(FormatKey))
                throw new Exception("File is not valid .ck2 format");

            content = content.Remove(0, FormatKey.Length + 1);
            content = content.Remove(content.LastIndexOf('}'), 1);

            return new Node.Node(content, 1);
        }

        public void Write(dynamic source) {
            StringBuilder builder = new StringBuilder();
            builder.Append(FormatKey + "\n");
            builder.Append(new NodeSerializer(source).Serialize());
            builder.Append("}");

            byte[] buffer = Encoding.UTF8.GetBytes(builder.ToString());
            using(FileStream writer = _file.Open(FileMode.Create)) 
                writer.Write(buffer, 0, buffer.Length);
        }

    }

}
