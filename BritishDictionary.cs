using System.Text.Json;

public class BritishDictionary : IWordsDictionary
{
    public IDictionary<string, string> Words { get; set; } = new Dictionary<string, string>();

    public void LoadDictionaryData()
    {
        string value = File.ReadAllText(Globals.DataPath, System.Text.Encoding.UTF8);
        var words = JsonSerializer.Deserialize<IDictionary<string, string>>(value);
        if (words != null) Words = words;
    }
}