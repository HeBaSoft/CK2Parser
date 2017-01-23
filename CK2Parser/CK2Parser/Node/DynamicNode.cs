using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Parser.Node {

    public class DynamicNode : DynamicObject {

        private Dictionary<string, object> _storage = new Dictionary<string, object>();

        public ulong Count { private set; get; }

        internal void Add(KeyValuePair<string, object> pair) {
            // Store multiple Models in List if they have same key
            if(_storage.ContainsKey(pair.Key)) {
                var entry = _storage[pair.Key];

                if(entry is List<object>)
                    (entry as List<object>).Add(pair.Value);
                else
                    _storage[pair.Key] = new List<object>() {
                        entry,
                        pair.Value
                    };
            } else {
                // Store single Model
                _storage.Add(pair.Key, pair.Value);
            }

            Count++;
        }

        public object Get(string key) {
            object entry = _storage[key];
            return entry is List<object> ? (entry as List<object>).ToArray() : entry;
        }

        // DynamicObject

        public override bool TrySetMember(SetMemberBinder binder, object value) {
            throw new NotImplementedException();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result) {
            if(!_storage.ContainsKey(binder.Name)) {
                result = null;
                return false;
            }

            result = Get(binder.Name);
            return true;
        }

    }

}
