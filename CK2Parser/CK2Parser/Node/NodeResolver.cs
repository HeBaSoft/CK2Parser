using CK2Parser.IO;
using CK2Parser.Parse;
using CK2Parser.Parse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Parser.Node {

    public class NodeResolver {

        private static readonly IParser[] Parsers = new IParser[] {
            new PrimitiveParser(),
            new ArrayParser(),
            new WrapperParser(),
            new ObjectParser()
        };

        private CachedLineReader  _reader;
        private DynamicNode _node = new DynamicNode();

        public NodeResolver(CachedLineReader reader) {
            _reader = reader;
        }

        public NodeResolver(string block) {
            _reader = new CachedLineReader(block);
        }

        public DynamicNode Resolve() {
            while(_reader.HasNext) {
                ValueHolder holder = ResolveNext();

                if(holder == null)
                    break; // hack

                _node.Add(holder);
            }

            _reader.Dispose();
            return _node;
        }

        private ValueHolder ResolveNext() {
            int startLine = _reader.CurrentLine;

            foreach(IParser parser in Parsers) {

                ValueHolder result = parser.Read(_reader);

                // If Parser fails, go back and try next one
                if(result == null) {
                    _reader.ReturnToLine(startLine);

                } else {
                    _reader.ClearBuffer();
                    return result;
                }

            }

            if(!_reader.ReadLine().Equals("}\n")) { // Hack for end of file
                throw new Exception("Failed to resolve model, no compatible parser found");
            } else {
                return null;
            }
        }

    }
}
