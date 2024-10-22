namespace SynonymSearchAPI.Data;

public class WordRequest
{
    public string Word { get; set; }
    public List<string> Synonyms { get; set; }
}