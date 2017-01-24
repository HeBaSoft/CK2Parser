using CK2Parser.Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Parser.Node {

    internal class ValueHolder {

        public string Key   {
            private set;
            get;
        }

        public object Value {
            set {
                _value = value;
            }
            get {
                if(!_isResolved) {
                    _value = new NodeResolver(_value as string).Resolve();
                    _isResolved = true;
                }

                return _value;
            }
        }

        private object  _value;
        private bool    _isResolved;
        private IParser _parser;

        public ValueHolder(IParser parser, string key, object value, bool isResolved = true) {
            _parser = parser;
            _isResolved = isResolved;
            _value = value;
            Key = key;

            if(!_isResolved && !(value is string))
                throw new Exception("Unresolved value has to be string");
        }

    }

}
