using CK2Parser.IO;
using CK2Parser.Parse;
using CK2Parser.Parse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Parser.Node {

    public class NodeDeserializer {

        private CachedLineReader _reader;
        private Node _node;

        public NodeDeserializer(CachedLineReader reader, Node node) {
            _reader = reader;
            _node = node;
        }

        public NodeDeserializer(string source, Node node) {
            _reader = new CachedLineReader(source);
            _node = node;
        }

        public Node Deserialize() {
            while(_reader.HasNext) {
                KeyValuePair<string, object> result = DeserializeNext();
                _node.Add(result.Key, result.Value);
            }

            _reader.Dispose();
            return _node;
        }

        private KeyValuePair<string, object> DeserializeNext() {
            int startLine = _reader.CurrentLine;

            foreach(IParser parser in CK2File.Parsers) {

                KeyValuePair<string, object> result = parser.Deserialize(_reader, _node.NestLevel);

                // If Parser fails, go back and try next one
                if(result.Equals(default(KeyValuePair<string, object>))) {
                    _reader.ReturnToLine(startLine);

                } else {
                    _reader.ClearBuffer();
                    return result;
                }

            }

            throw new Exception("Failed to resolve model, no compatible parser found");
        }

    }
}
