using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CK2Parser.IO {

    public class CachedLineReader : IDisposable {

        public int CurrentLine { private set; get; } = 1;

        public bool HasNext {
            get {
                string line = ReadLine();
                CurrentLine--;
                return line != null;
            }
        }

        private StreamReader            _reader;
        private Dictionary<int, string> _buffer = new Dictionary<int, string>();
        private int                     _pointer = 1;

        public CachedLineReader(StreamReader reader) {
            _reader = reader;
        }

        public CachedLineReader(string text) {
            _reader = new StreamReader(
                new MemoryStream(
                    Encoding.UTF8.GetBytes(text)
                )
            );
        }

        public string ReadLine() {
            string line = null;

            if(CurrentLine == _pointer) {
                do {
                    line = _reader.ReadLine();
                    if(line == null) return line;
                } while(line == string.Empty); // Skip empty lines

                _buffer.Add(CurrentLine, line);
                _pointer++;

            } else if(CurrentLine < _pointer && _buffer.ContainsKey(CurrentLine)) {
                line = _buffer[CurrentLine];

            }

            if(line != null) {
                line += '\n';
                CurrentLine++;
            }

            return line;
        }

        public void ReturnToLine(int line) {
            if(line > _pointer)
                throw new Exception("Can not return to line that was never read");

            CurrentLine = line;
        }

        public void ClearBuffer() {
            _buffer.Clear();
        }

        // IDisposable

        public void Dispose() {
            ClearBuffer();
            _reader.Dispose();
        }
    }

}
