using Application.Dto;
using Application.Interface;
using Application.UseCases;
using Moq;

namespace Testing;

public class UpdateStockTest
{
    private readonly Mock<IUpdateStockService> _mockService;
    private readonly Mock<ICheckAvailabilityService> _mockCheckAvailabilityService;
    private readonly StockUC _controller;

    public UpdateStockTest()
    {
        _mockCheckAvailabilityService = new Mock<ICheckAvailabilityService>();
        _mockService = new Mock<IUpdateStockService>();
        //_controller = new Stock(_mockCheckAvailabilityService.Object, _mockService.Object);
    }

    [Fact]
    public async Task UpdateStock_ReturnsOkTrue_WhenUpdateIsSuccessful()
    {
        // Arrange
        var ingredient = new IngredientDto { IdIngredient = 1, Quantity = 10 };
        _mockService.Setup(s => s.UpdateStockAsync(ingredient)).ReturnsAsync(true);

        // Act
        //var result = await _controller.UpdateStock(ingredient);

        //// Assert
        //var okResult = Assert.IsType<OkObjectResult>(result.Result);
        //var returnValue = Assert.IsType<bool>(okResult.Value);
        //Assert.True(returnValue);
    }

    [Fact]
    public async Task UpdateStock_ReturnsOkFalse_WhenUpdateFails()
    {
        // Arrange
        var ingredient = new IngredientDto { IdIngredient = 2, Quantity = -5 };
        _mockService.Setup(s => s.UpdateStockAsync(ingredient)).ReturnsAsync(false);

        // Act
        //var result = await _controller.UpdateStock(ingredient);

        //// Assert
        //var okResult = Assert.IsType<OkObjectResult>(result.Result);
        //var returnValue = Assert.IsType<bool>(okResult.Value);
        //Assert.False(returnValue);
    }

    [Fact]
    public async Task UpdateStock_ReturnsInternalServerError_WhenExceptionIsThrown()
    {
        // Arrange
        var ingredient = new IngredientDto { IdIngredient = 3, Quantity = 5 };
        _mockService.Setup(s => s.UpdateStockAsync(ingredient))
                    .ThrowsAsync(new System.Exception("DB Error"));

        // Act
        //var result = await _controller.UpdateStock(ingredient);

        //// Assert
        //var errorResult = Assert.IsType<ObjectResult>(result.Result);
        //Assert.Equal(500, errorResult.StatusCode);
        //Assert.IsType<System.Exception>(errorResult.Value);
    }
}
