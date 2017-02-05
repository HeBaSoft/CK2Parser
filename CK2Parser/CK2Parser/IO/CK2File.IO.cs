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

        internal static readonly IParser[] Parsers = new IParser[] {
            new PrimitiveParser(),
            new ArrayParser(),
            new WrapperParser(),
            new NodeParser()
        };

        private static readonly string FormatKey = "CK2txt";

        // Dynamic IO

        public CK2File Save() {
            Write(_file.FullName, _node);
            return this;
        }

        public CK2File SaveAs(string path) {
            Write(path, _node);
            return this;
        }

        // Static IO

        public static CK2File Read(string path) {
            FileInfo file = new FileInfo(path);

            // Make sure file exists
            if(!file.Exists)
                throw new Exception(string.Format("File {0} does not exist", file.FullName));

            // Read file
            string content;
            using(StreamReader reader = file.OpenText())
                content = reader.ReadToEnd();

            // Verify format
            if(!content.StartsWith(FormatKey))
                throw new Exception("File is not valid .ck2 format");

            // Strip format stuff
            content = content.Remove(0, FormatKey.Length + 1);
            content = content.Remove(content.LastIndexOf('}'), 1);

            return new CK2File(file, new Node.Node(content, 1));
        }

        public static void Write(string path, dynamic data) {
            StringBuilder builder = new StringBuilder();

            // Add formating stuff and serialize data
            builder.Append(FormatKey + "\n");
            builder.Append(new NodeSerializer(data).Serialize());
            builder.Append("}");

            // Write to file
            byte[] buffer = Encoding.UTF8.GetBytes(builder.ToString());
            using(FileStream writer = File.Open(path, FileMode.Create))
                writer.Write(buffer, 0, buffer.Length);
        }

    }

}
