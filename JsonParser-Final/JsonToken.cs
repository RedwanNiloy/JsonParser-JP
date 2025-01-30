using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonParser_Final
{
    public class JsonToken
    {
        public JsonTokenType Type { get; }
        public string Value { get; }

        public JsonToken(JsonTokenType type, string value = "")
        {
            Type = type;
            Value = value;
        }

        public override string ToString() => $"{Type}: {Value}";
    }
}
