using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonParser_Final
{
    public class JsonTokenizer
    {
        private readonly string _json;
        private int _position;

        public JsonTokenizer(string json)
        {
            _json = json;
            _position = 0;
        }

        public List<JsonToken> Tokenize()
        {
            var tokens = new List<JsonToken>();
            while (_position < _json.Length)
            {
                char current = _json[_position];

                if (char.IsWhiteSpace(current))
                {
                    _position++;
                    continue;
                }

                switch (current)
                {
                    case '{': tokens.Add(new JsonToken(JsonTokenType.LeftBrace, "{")); break;
                    case '}': tokens.Add(new JsonToken(JsonTokenType.RightBrace, "}")); break;
                    case '[': tokens.Add(new JsonToken(JsonTokenType.LeftBracket, "[")); break;
                    case ']': tokens.Add(new JsonToken(JsonTokenType.RightBracket, "]")); break;
                    case ':': tokens.Add(new JsonToken(JsonTokenType.Colon, ":")); break;
                    case ',': tokens.Add(new JsonToken(JsonTokenType.Comma, ",")); break;
                    case '"': tokens.Add(ReadString()); break;
                    default:
                        if (char.IsDigit(current) || current == '-')
                            tokens.Add(ReadNumber());
                        else if (char.IsLetter(current))
                            tokens.Add(ReadKeyword());
                        else
                            throw new Exception($"Unexpected character '{current}' at position {_position}");
                        continue;
                }
                _position++;
            }

            tokens.Add(new JsonToken(JsonTokenType.EOF));
            return tokens;
        }

        private JsonToken ReadString()
        {
            var sb = new StringBuilder();
            _position++;  // Skip opening quote
            while (_position < _json.Length && _json[_position] != '"')
            {
                sb.Append(_json[_position]);
                _position++;
            }
            _position++;  // Skip closing quote
            return new JsonToken(JsonTokenType.String, sb.ToString());
        }

        private JsonToken ReadNumber()
        {
            var sb = new StringBuilder();
            while (_position < _json.Length && (char.IsDigit(_json[_position]) || _json[_position] == '.'))
            {
                sb.Append(_json[_position]);
                _position++;
            }
            return new JsonToken(JsonTokenType.Number, sb.ToString());
        }

        private JsonToken ReadKeyword()
        {
            var sb = new StringBuilder();
            while (_position < _json.Length && char.IsLetter(_json[_position]))
            {
                sb.Append(_json[_position]);
                _position++;
            }

            string word = sb.ToString();
            return word switch
            {
                "true" => new JsonToken(JsonTokenType.True, word),
                "false" => new JsonToken(JsonTokenType.False, word),
                "null" => new JsonToken(JsonTokenType.Null, word),
                _ => throw new Exception($"Unexpected keyword '{word}'")
            };
        }
    }

}
