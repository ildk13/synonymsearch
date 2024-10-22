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
        var request1 = new WordRequest
        {
            Word = "clean",
            Synonyms = new List<string> { "wash" }
        };

        var request2 = new WordRequest
        {
            Word = "wash",
            Synonyms = new List<string> { "tidy" }
        };

        // Act
        sut.AddSynonym(request1);
        var result = sut.GetSynonyms("clean");

        // Assert
        result.Should().BeOfType<HashSet<string>>();
        result.Should().Contain("wash");
        
        // Act 2
        sut.AddSynonym(request2);
        var result2 = sut.GetSynonyms("clean");
        
        result2.Should().Contain("tidy");
    }
}