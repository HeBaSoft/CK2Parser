﻿using CK2Parser.IO;
using CK2Parser.Parse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Parser.Node {

    public class NodeSerializer {

        private StringBuilder _buider = new StringBuilder();
        private Node _node;

        public NodeSerializer(Node node) {
            _node = node;
        }

        public string Serialize() {
            foreach(KeyValuePair<string, object> pair in _node) {

                if(pair.Value is IList && !pair.Value.GetType().IsArray) {
                    foreach(object entry in pair.Value as IList)
                        SerializePair(new KeyValuePair<string, object>(pair.Key, entry));
                } else {
                    SerializePair(pair);
                }

            }

            return _buider.ToString();
        }

        public void SerializePair(KeyValuePair<string, object> pair) {
            foreach(IParser parser in CK2File.Parsers) {

                if(parser.Serialize(pair, _buider, _node.NestLevel)) {
                    return;
                }

            }

            throw new Exception(string.Format("Unable to serialize data type {0}", pair.Value.GetType()));
        }

    }

}
