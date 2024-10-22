namespace SynonymSearchAPI.Data;

public interface ISynonymService
{
    void AddSynonym(WordRequest request);

    HashSet<string> GetSynonyms(string word);     
}    
