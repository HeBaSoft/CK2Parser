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

        internal void Add(ValueHolder holder) {
            // Store multiple Models in List if they have same key
            if(_storage.ContainsKey(holder.Key)) {
                var entry = _storage[holder.Key];

                if(entry is List<ValueHolder>)
                    (entry as List<ValueHolder>).Add(holder);
                else
                    _storage[holder.Key] = new List<ValueHolder>() {
                        entry as ValueHolder,
                        holder
                    };
            } else {
                // Store single Model
                _storage.Add(holder.Key, holder);
            }

            Count++;
        }

        public object Get(string key) {
            object entry = _storage[key];

            return entry is List<ValueHolder> ? 
                (entry as List<ValueHolder>).Select(e => e.Value).ToArray() : 
                (entry as ValueHolder).Value;
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
