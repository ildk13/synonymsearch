using FluentAssertions;

namespace SynonymSearchAPI.Data.Tests;

public class SynonymServiceTests
{
    private readonly SynonymService sut;

    public SynonymServiceTests()
    {
        sut = new SynonymService();
    }

    [Fact]
    public void GetSynonyms_ReturnsNotFound_WhenWordDoesNotExist()
    {
            // Act
            var word = "nonexistent";
            var result = () => sut.GetSynonyms(word);

            // Assert
            result.Should().ThrowExactly<Exception>().Which.Message.Should().Be($"Couldn't find synonyms to the word: {word}");
    }

    [Fact]
    public void AddWord_AddsSynonymsSuccessfully()
    {
        // Arrange
        var request = new WordRequest()
        {
            Word = "clean",
            Synonyms = new List<string> { "wash", "tidy" }
        };

        // Act
        sut.AddSynonym(request);
        var result = sut.GetSynonyms("clean");

        // Assert
        result.Should().BeOfType<HashSet<string>>();
        result.Should().Contain("wash");
        result.Should().Contain("tidy");
    }

    [Fact]
    public void AddWord_AddsMoreSynonymsToSameWord()
    {
        // Arrange
        var request = new WordRequest
        {
            Word = "A",
            Synonyms = new List<string> { "B" }
        };

        var request2 = new WordRequest
        {
            Word = "B",
            Synonyms = new List<string> { "C" }
        };

        
        // Act 2
        sut.AddSynonym(request);
        sut.AddSynonym(request2);

        var result2 = sut.GetSynonyms("A");
        result2.Should().Contain("C");
    }
}