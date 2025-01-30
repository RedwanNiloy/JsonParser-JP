// See https://aka.ms/new-console-template for more information

using JsonParser_Final;

string json = "{\"name\": \"John\", \"age\": 30, \"isStudent\": false}";
JsonTokenizer tokenizer = new JsonTokenizer(json);
List<JsonToken> tokens = tokenizer.Tokenize();

foreach (var token in tokens)
{
    Console.WriteLine(token);
}

JsonParser parser = new JsonParser(tokens);
var parsedJson = parser.Parse();

Console.WriteLine(parsedJson);

if (parsedJson is Dictionary<string, object> dict)
{
    foreach (var kvp in dict)
    {
        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
    }
}
