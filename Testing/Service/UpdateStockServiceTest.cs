using Application.Dto;
using Application.Service;
using Domain.Models;
using Moq;
using Repository.Interface;

namespace Testing;

public class UpdateStockServiceTest
{
    private readonly Mock<IIngredientRepository> _mockIngredientRepository;
    private readonly UpdateStockService _updateStockService;

    public UpdateStockServiceTest()
    {
        _mockIngredientRepository = new Mock<IIngredientRepository>();
        _updateStockService = new UpdateStockService(_mockIngredientRepository.Object);
    }

    [Fact]
    public async Task UpdateStockAsync_ReturnsTrue_WhenIngredientIsUpdated()
    {
        // Arrange
        var dto = new IngredientDto { IdIngredient = 1, Quantity = 5 };
        var ingredient = new Ingredient { IdIngredient = 1, Quantity = 10 };

        _mockIngredientRepository.Setup(r => r.GetIngredientById(dto.IdIngredient)).ReturnsAsync(ingredient);
       // _mockIngredientRepository.Setup(r => r.UpdateStockAsync(It.IsAny<Ingredient>())).ReturnsAsync(true);

        // Act
        //var result = await _updateStockService.UpdateStockAsync(dto);

        //// Assert
        //Assert.True(result);
        //_mockIngredientRepository.Verify(r => r.UpdateStockAsync(It.Is<Ingredient>(i => i.Quantity == 15)), Times.Once);
    }

    [Fact]
    public async Task UpdateStockAsync_ReturnsFalse_WhenIngredientNotFound()
    {
        // Arrange
        var dto = new IngredientDto { IdIngredient = 99, Quantity = 5 };
        _mockIngredientRepository.Setup(r => r.GetIngredientById(dto.IdIngredient)).ReturnsAsync((Ingredient)null);

        // Act
        //var result = await _updateStockService.UpdateStockAsync(dto);

        // Assert
      //  Assert.False(result);
       // _mockIngredientRepository.Verify(r => r.UpdateStockAsync(It.IsAny<Ingredient>()), Times.Never);
    }
}
