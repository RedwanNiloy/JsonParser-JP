// See https://aka.ms/new-console-template for more information

using JsonParser_Final;

string json = "{\"name\" : \"John\", \"age\": 30, \"isStudent\": false}";






//Console.WriteLine(json);    
JsonTokenizer tokenizer = new JsonTokenizer(json);
List<JsonToken> tokens = tokenizer.Tokenize();

//Console.WriteLine(tokens[2]);  
/*foreach (var token in tokens)
{
    Console.WriteLine(token);
}*/

JsonParser parser = new JsonParser(tokens);
var parsedJson = parser.Parse();

//Console.WriteLine(parsedJson);

if (parsedJson is Dictionary<string, object> dict)
{
    Console.WriteLine("Hello");
    foreach (var kvp in dict)
    {
        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
    }
}
