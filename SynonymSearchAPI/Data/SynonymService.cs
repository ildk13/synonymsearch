
namespace SynonymSearchAPI.Data;
public class SynonymService : ISynonymService
{
    private static Dictionary<string, HashSet<string>> synonyms = new Dictionary<string, HashSet<string>>();

    public HashSet<string> GetSynonyms(string word)
    {
        if (synonyms.ContainsKey(word))
        {
            return synonyms[word];
        }

        throw new Exception($"Couldn't find synonyms to the word: {word}");
    }

    public void AddSynonym(WordRequest request)
    {
        foreach (var synonym in request.Synonyms)
        {
            AddSynonym(request.Word, synonym);
            ApplyTransitiveRule(request.Word);              
        }
    }

    private void AddSynonym(string word, string synonym)
    {
        if (!synonyms.ContainsKey(word))
        {
            synonyms[word] = new HashSet<string>();
        }

        synonyms[word].Add(synonym);

        if (!synonyms.ContainsKey(synonym))
        {
            synonyms[synonym] = new HashSet<string>();
        }

        synonyms[synonym].Add(word);
    }

    private void ApplyTransitiveRule(string word)
    {
        if (!synonyms.ContainsKey(word)) 
        {
            return;
        }

        var allSynonyms = new HashSet<string>(synonyms[word]);
        var toProcess = new Queue<string>(allSynonyms);

        while (toProcess.Count > 0)
        {
            var current = toProcess.Dequeue();
            foreach (var synonym in synonyms[current])
            {
                if (allSynonyms.Add(synonym))
                {
                    toProcess.Enqueue(synonym);
                    AddSynonym(word, synonym);
                }
            }
        }
    }
}