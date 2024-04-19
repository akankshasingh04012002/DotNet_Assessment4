
using rpgAPI.Model;
using rpgAPI.Service;

namespace rpgAPITest
{

public class CharacterServiceTest
{
    [Fact]
    public void GetAllCharacterGivenValidRequestGetResult()
    {
        //Arrange
        var cs = new CharacterService();

        //Act
        var result = cs.GetAllCharacter();

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void AddCharacterGivenValidRequestGetResult()
    {
        //Arrange
        var character = new Character()
        {
            Name = "Unit Test",
            Id = 4
        };

        var cs = new CharacterService();

        // Act
        var result = cs.AddCharacter(character);
        
        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void GetCharacterByIdGivenValidRequestGetResult()
    {
        // Arrange
        var cs = new CharacterService();

        // Act
        var result = cs.GetCharacterById(0);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void UpdateCharacterGivenValidRequestGetResult()
    {
        // Arrange
        var character = new Character()
        {
            Name = "Update Character",
            Id = 5,
        };

        var cs = new CharacterService();

        // Act
        var result = cs.UpdateCharacter(character);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void UpdateCharacterGivenValidRequestOldCharacterUpdate()
    {
        // Arrange
        var character = new Character()
        {
            Name = "Update Character",
            Id = 0,
        };

        var cs = new CharacterService();

        // Act
        var result = cs.UpdateCharacter(character);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data.FirstOrDefault(x => x.Name == "Update Character"));
    }

    [Fact]
    public void DeleteCharacterGivenValidId_ReturnsSuccessResponse()
    {
        // Arrange
        var existingId = 1;

        var characterService = new CharacterService();

        var initialCount = characterService.GetAllCharacter().Data.Count;

        // Act
        var result = characterService.DeleteCharacter(existingId);

        // Assert
        Assert.True(result.Success);
        Assert.Null(result.Message);
        Assert.Equal(initialCount - 1, result.Data.Count);
        Assert.Null(characterService.GetAllCharacter().Data.FirstOrDefault(c => c.Id == existingId));
    }



    [Fact]
    public void DeleteCharacterGivenInvalidId_ReturnsFailureResponse()
    {
        // Arrange
        var invalidId = 999;

        var characterService = new CharacterService();

        // Act
        var result = characterService.DeleteCharacter(invalidId);

        // Assert
        Assert.False(result.Success);
        Assert.NotNull(result.Message);
        Assert.Equal("Character not found", result.Message);
    }

}

}