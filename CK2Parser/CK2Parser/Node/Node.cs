using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Parser.Node {

    public class Node : DynamicObject, IEnumerable {

        private Dictionary<string, object> _storage;

        internal string Raw         { private set; get; }

        public bool     IsResolved  { private set; get; }
        public int      NestLevel   { private set; get; }
        public int      Elements    { private set; get; }

        public Node(string raw, int nestLevel) {
            IsResolved = false;
            NestLevel = nestLevel;
            Raw = raw;
        }

        // Internals

        internal void Add(string key, object value) {
            // Store multiple Models in List if they have same key
            if(_storage.ContainsKey(key)) {
                var entry = _storage[key];

                if(entry is List<object>)
                    (entry as List<object>).Add(value);
                else
                    _storage[key] = new List<object>() { entry, value };
            } else {
                // Store single Model
                _storage.Add(key, value);
            }

            Elements++;
        }

        private void Resolve() {
            if(IsResolved)
                return;

            _storage = new Dictionary<string, object>();
            new NodeDeserializer(this).Deserialize();
            Raw = null;
            IsResolved = true;
        }

        // Unsafe

        public object Get(string key) {
            Resolve();

            if(!_storage.ContainsKey(key))
                return null;

            return _storage[key];
        }

        public bool Set(string key, object value) {
            Resolve();

            if(!_storage.ContainsKey(key))
                return false;

            _storage[key] = value;
            return true;
        }

        // DynamicObject

        public override bool TrySetMember(SetMemberBinder binder, object value) {
            return Set(binder.Name, value);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object value) {
            value = Get(binder.Name);
            return value != null;
        }

        // IEnumerable

        public IEnumerator GetEnumerator() {
            Resolve();
            return _storage.GetEnumerator();
        }

    }

}
