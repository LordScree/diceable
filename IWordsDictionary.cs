public interface IWordsDictionary
{
    void LoadDictionaryData();

    public IDictionary<string,string> Words { get; set; }
}