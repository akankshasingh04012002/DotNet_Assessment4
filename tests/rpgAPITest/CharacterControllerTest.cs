using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AutoFixture;
using rpgAPI.Controller;
using rpgAPI.Model;
using rpgAPI.Service;
using Xunit;

namespace rpgAPITest
{
    public class CharacterControllerTest
    {
        public Mock<ICharacterService> mockService = new Mock<ICharacterService>();

        [Fact]
        public void GetCharacterReturnsListOfCharactersSuccess()
        {
            //Arrange
            var cList = new List<Character>()
            {
                new Character(),
                new Character() { Name = "Gollum",Id = 1},

            };

            var serviceResponse = new ServiceResponse<List<Character>>()
            {
                Data = cList
            };

            mockService.Setup(x=>x.GetAllCharacter()).Returns(serviceResponse);

            var charController = new CharacterController(mockService.Object);

            //Act
            var result = charController.GetCharacter();

            var okResult = (ObjectResult)result.Result;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetCharacterByIdWithValidIdReturnsCharacterSuccess()
        {
            //Arrange
            var fixture = new Fixture();
            
            var character = fixture.Create<Character>();

            var serviceResponse = new ServiceResponse<Character>()
            {
                Data = character
            };

            mockService.Setup(x => x.GetCharacterById(0)).Returns(serviceResponse);

            var charController = new CharacterController(mockService.Object);

            var result = charController.GetId(0);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void PostCharacterWithValidCharacterReturnsSuccessResponse()
        {
            // Arrange
            var characterToAdd = new Character { Name = "Frodo", Id = 2 };
            var serviceResponse = new ServiceResponse<List<Character>> { Data = new List<Character> { characterToAdd } };
            mockService.Setup(x => x.AddCharacter(characterToAdd)).Returns(serviceResponse);
            var charController = new CharacterController(mockService.Object);

            // Act
            var result = charController.PostCharacter(characterToAdd);

           // Assert
           var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<Character>>>>(result);
           var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
           var serviceResponseResult = Assert.IsType<ServiceResponse<List<Character>>>(okResult.Value);
           Assert.True(serviceResponseResult.Success);
           Assert.NotNull(serviceResponseResult.Data);
           Assert.Contains(characterToAdd, serviceResponseResult.Data);
        }


        [Fact]
        public void UpdateCharacterWithValidCharacterReturnsSuccessResponse()
        {

            // Arrange
            var characterToUpdate = new Character { Name = "Sam", Id = 3 };
            var serviceResponse = new ServiceResponse<List<Character>> { Data = new List<Character> { characterToUpdate } };
            mockService.Setup(x => x.UpdateCharacter(characterToUpdate)).Returns(serviceResponse);
            var charController = new CharacterController(mockService.Object);

            // Act
            var result = charController.UpdateCharacter(characterToUpdate);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<Character>>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var serviceResponseResult = Assert.IsType<ServiceResponse<List<Character>>>(okResult.Value);
            Assert.True(serviceResponseResult.Success);
            Assert.NotNull(serviceResponseResult.Data);
            Assert.Contains(characterToUpdate, serviceResponseResult.Data);
        }


        [Fact]
        public void DeleteCharacterWithValidIdReturnsSuccessResponse()
        {
            // Arrange
            int idToDelete = 1;

            mockService.Setup(service => service.DeleteCharacter(idToDelete)).Returns(new ServiceResponse<List<Character>>
            {
            Success = true,
            Message = null
            });

            var controller = new CharacterController(mockService.Object);

            // Act
            var result = controller.DeleteCharacter(idToDelete);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ServiceResponse<List<Character>>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var serviceResponse = Assert.IsType<ServiceResponse<List<Character>>>(okResult.Value);
            Assert.True(serviceResponse.Success);
            Assert.Null(serviceResponse.Message);
        }

    }
}

