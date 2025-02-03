using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser_Final
{
    public class JsonParser
    {
        private readonly List<JsonToken> _tokens;
        private int _position;
        private int _oldpos;

        public JsonParser(List<JsonToken> tokens)
        {
            _tokens = tokens;
            _position = 0;

            
        }

        public object Parse()
        {
            return ParseValue();
        }

        private object ParseValue()
        {
            var token = _tokens[_position];

           //Console.WriteLine(token.Value);    
            switch (token.Type)
            {
                case JsonTokenType.String: return Consume().Value;
                case JsonTokenType.Number: return double.Parse(Consume().Value);
                case JsonTokenType.True: _position++; return true;
                case JsonTokenType.False: _position++; return false;
                case JsonTokenType.Null: return null;
                case JsonTokenType.LeftBrace: return ParseObject();
                case JsonTokenType.LeftBracket: return ParseArray();
               // case JsonTokenType.EOF: return "EOF";
                default: throw new Exception($"Unexpected token: {token}");
            }
        }

        private JsonToken Consume()
        {
           // Console.WriteLine(_tokens[_position++]);
            return _tokens[_position++];
        }

        private Dictionary<string, object> ParseObject()
        {
            var obj = new Dictionary<string, object>();



                Consume(); // Consume '{'
                while (_tokens[_position].Type != JsonTokenType.RightBrace)
                {


                   // Console.WriteLine(_tokens[_position]);
                    var key = Consume().Value;



                    Consume();

                    var value = ParseValue();
                    obj[key] = value;



                if (_tokens[_position].Type == JsonTokenType.Comma)
                { Consume(); } // Consume ','

               



            }
           
            return obj;
        }

        private List<object> ParseArray()
        {
            var list = new List<object>();
            Consume(); // Consume '['
            while (_tokens[_position].Type != JsonTokenType.RightBracket)
            {
                list.Add(ParseValue());
                if (_tokens[_position].Type == JsonTokenType.Comma)
                    Consume(); // Consume ','
            }
            Consume(); // Consume ']'
            return list;
        }
    }

}
