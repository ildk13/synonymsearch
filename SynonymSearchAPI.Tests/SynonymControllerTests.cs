using SynonymSearchAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Moq;
using SynonymSearchAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SynonymSearchAPI.Tests;

public class SynonymControllerTests
{

    private readonly Mock<ISynonymService> synonymService;
    private readonly SynonymController sut;

    public SynonymControllerTests()
    {
        synonymService = new Mock<ISynonymService>();
        sut = new SynonymController(synonymService.Object);        
    }

    [Fact]
    public void Ctor_GivenNullService_ThrowsException()
    {
        // Act
        var result = () => new SynonymController(null);
        
        //Assert
        result.Should().ThrowExactly<ArgumentNullException>().Which.ParamName.Should().Be("synonymService");
    }

    [Fact]
    public void GetSynonyms_ReturnsNotFound_WhenWordDoesNotExist()
    {
            //Arrange
            var word = "nonexistent";
            synonymService.Setup(s => s.GetSynonyms(word)).Throws<Exception>();

            // Act
            var result = sut.GetSynonyms(word);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public void GetSynonyms_GivenExistsWord_ReturnsOk()
    {
            //Arrange
            var expected = new HashSet<string>{ "wash", "tidy" };
            var word = "clean";
            synonymService.Setup(s => s.GetSynonyms(word)).Returns(expected);

            // Act
            var result = sut.GetSynonyms(word) as OkObjectResult;

            // Assert
            if (result != null)
            {
                result.Value.Should().BeEquivalentTo(expected);
            }
            else Assert.Fail();
            
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

        synonymService.Setup(s => s.AddSynonym(request));

        // Act
        var result = sut.AddWord(request);

        // Assert
        result.Should().BeOfType<OkResult>();
    }
}