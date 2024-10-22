
namespace SynonymSearchAPI.Data;
public class SynonymService : ISynonymService
{
    private static Dictionary<string, HashSet<string>> synonyms = new Dictionary<string, HashSet<string>>();

    public HashSet<string> GetSynonyms(string word)
    {
        if (synonyms.ContainsKey(word))
        {
            return GetAllSynonymsForWord(word);
        }

        throw new Exception($"Couldn't find synonyms to the word: {word}");
    }

    public void AddSynonym(WordRequest request)
    {
        foreach (var synonym in request.Synonyms)
        {
            AddSynonym(request.Word, synonym);
            //ApplyTransitiveRule(request.Word);              
        }
    }

    private void AddSynonym(string word, string synonym)
    {
        if (!synonyms.ContainsKey(word))
        {
            synonyms[word] = new HashSet<string>();
        }

        if (!synonyms.ContainsKey(synonym))
        {
            synonyms[synonym] = new HashSet<string>();
        }

        synonyms[word].Add(synonym);
        synonyms[synonym].Add(word);
    }

    private HashSet<string> GetAllSynonymsForWord(string word)
    {
        var result = new HashSet<string>();
        if (!synonyms.ContainsKey(word))
        {
            return result;
        }


        GetAllSynonymsForWordDFS(word, result, new HashSet<string>());
        return result;

    }

    private void GetAllSynonymsForWordDFS(string currectWord, HashSet<string> result, HashSet<string> visited)
    {
        result.Add(currectWord);
        visited.Add(currectWord);

        foreach (var synonym in synonyms[currectWord])
        {
            if (!visited.Contains(synonym))
            {
                GetAllSynonymsForWordDFS(synonym, result, visited);
            }
        }
    }
}