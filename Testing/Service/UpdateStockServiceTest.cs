using Application.Dto;
using Application.Service;
using Domain.Models;
using Moq;
using Repository.Interface;
using Xunit;

public class UpdateStockServiceTests
{
    private readonly Mock<IIngredientRepository> _ingredientRepositoryMock;
    private readonly UpdateStockService _updateStockService;

    public UpdateStockServiceTests()
    {
        _ingredientRepositoryMock = new Mock<IIngredientRepository>();
        _updateStockService = new UpdateStockService(_ingredientRepositoryMock.Object);
    }

    [Fact]
    public async Task UpdateStockAsync_ShouldReturnTrue_WhenStockIsUpdatedSuccessfully()
    {
        // Arrange
        var ingredients = new List<IngredientDto>
        {
            new IngredientDto { IdIngredient = 1, Quantity = 5 },
            new IngredientDto { IdIngredient = 2, Quantity = 3 }
        };

        _ingredientRepositoryMock
            .Setup(repo => repo.GetIngredientById(It.IsAny<int>()))
            .ReturnsAsync((int id) => new Ingredient { IdIngredient = id, Quantity = 10 });

        _ingredientRepositoryMock
            .Setup(repo => repo.UpdateStockAllAsync(It.IsAny<List<Ingredient>>()))
            .ReturnsAsync(true);

        // Act
        var result = await _updateStockService.UpdateStockAsync(ingredients);

        // Assert
        Assert.True(result);
        //_ingredientRepositoryMock.Verify(repo => repo.UpdateStockAllAsync(It.IsAny<List<Ingredient>>()), Times.Once);
    }

    [Fact]
    public async Task UpdateStockAsync_ShouldReturnFalse_WhenStockUpdateFails()
    {
        // Arrange
        var ingredients = new List<IngredientDto>
        {
            new IngredientDto { IdIngredient = 1, Quantity = 5 }
        };

        _ingredientRepositoryMock
            .Setup(repo => repo.GetIngredientById(It.IsAny<int>()))
            .ReturnsAsync(new Ingredient { IdIngredient = 1, Quantity = 10 });

        _ingredientRepositoryMock
            .Setup(repo => repo.UpdateStockAllAsync(It.IsAny<List<Ingredient>>()))
            .ReturnsAsync(false);

        // Act
        var result = await _updateStockService.UpdateStockAsync(ingredients);

        // Assert
        Assert.False(result);
    }
}
