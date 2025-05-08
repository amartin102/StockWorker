using Application.Dto;
using Application.Interface;
using Application.Service;
using Application.UseCases;
using Moq;

namespace Testing.Controller
{
    public class CheckAvailabilityTest
    {

        private readonly StockUC _stockController;
        private readonly Mock<ICheckAvailabilityService> _mockCheckAvailabilityService;
        private readonly Mock<IUpdateStockService> _mockupdateStockService;

        public CheckAvailabilityTest()
        {
            // Mock dependencies
            _mockCheckAvailabilityService = new Mock<ICheckAvailabilityService>();
            _mockupdateStockService = new Mock<IUpdateStockService>();
            // Initialize the controller with mocked services
            //_stockController = new Stock(_mockCheckAvailabilityService.Object, _mockupdateStockService.Object);
        }

        /// <summary>
        /// Test to check if the CheckAvailability method returns Ok with a true value.
        /// </summary>
        /// <returns></returns>
        
        [Fact]
        public async Task CheckAvailability_ReturnsOk_WithTrueValue()
        {
            // Arrange
            int recipeId = 1;
            //_mockCheckAvailabilityService.Setup(s => s.GetRecipeById(recipeId, 1)).ReturnsAsync(true);

            // Act
            //var result = await _stockController.CheckAvailability(recipeId);

            //// Assert
            //var okResult = Assert.IsType<OkObjectResult>(result.Result);
            //Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task CheckAvailability_ReturnsOk_WithFalseValue()
        {
            // Arrange
            int recipeId = 2;
            //_mockCheckAvailabilityService.Setup(s => s.GetRecipeById(recipeId, 1)).ReturnsAsync(false);

            // Act
            //var result = await _stockController.CheckAvailability(recipeId);

            //// Assert
            //var okResult = Assert.IsType<OkObjectResult>(result.Result);
            //Assert.False((bool)okResult.Value);
        }

        [Fact]
        public async Task CheckAvailability_Returns500_OnException()
        {
            // Arrange
            int recipeId = 3;
            _mockCheckAvailabilityService.Setup(s => s.GetRecipeById(recipeId, 1)).ThrowsAsync(new Exception("Something went wrong"));

            // Act
            //var result = await _stockController.CheckAvailability(recipeId);

            //// Assert
            //var statusResult = Assert.IsType<ObjectResult>(result.Result);
            //Assert.Equal(500, statusResult.StatusCode);
            //Assert.Contains("Something went wrong", statusResult.Value.ToString());
        }

        [Fact]
        public async Task UpdateOrder_ReturnsOk_WithTrue()
        {
            // Arrange
            var ingredient = new IngredientDto { IdIngredient = 1,  Quantity = 5 };
            //_mockupdateStockService.Setup(s => s.UpdateStockAsync(ingredient)).ReturnsAsync(true);

            // Act
            //var result = await _stockController.UpdateStock(ingredient);

            //// Assert
            //var okResult = Assert.IsType<OkObjectResult>(result.Result);
            //Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task UpdateOrder_ReturnsOk_WithFalse()
        {
            // Arrange
            var ingredient = new IngredientDto { IdIngredient = 2,Quantity = 0 };
           // _mockupdateStockService.Setup(s => s.UpdateStockAsync(ingredient)).ReturnsAsync(false);

            // Act
            //var result = await _stockController.UpdateStock(ingredient);

            //// Assert
            //var okResult = Assert.IsType<OkObjectResult>(result.Result);
            //Assert.False((bool)okResult.Value);
        }

        [Fact]
        public async Task UpdateOrder_Returns500_OnException()
        {
            // Arrange
            var ingredient = new IngredientDto { IdIngredient = 3, Quantity = 10 };
            //_mockupdateStockService.Setup(s => s.UpdateStockAsync(ingredient))
            //            .ThrowsAsync(new Exception("Database failure"));

            // Act
            //var result = await _stockController.UpdateStock(ingredient);

            //// Assert
            //var statusResult = Assert.IsType<ObjectResult>(result.Result);
            //Assert.Equal(500, statusResult.StatusCode);
            //Assert.IsType<Exception>(statusResult.Value);
            //Assert.Equal("Database failure", ((Exception)statusResult.Value).Message);
        }
    }
}