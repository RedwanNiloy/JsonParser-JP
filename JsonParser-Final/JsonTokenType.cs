using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonParser_Final
{
    public enum JsonTokenType
    {
        LeftBrace, RightBrace, LeftBracket, RightBracket, Colon, Comma,
        String, Number, True, False, Null, EOF
    }
}
