using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interface;
using Application.UseCases;
using Moq;
using Xunit;

namespace Testing.UseCases
{
    public class StockUCTests
    {
        private readonly Mock<ICheckAvailabilityService> _mockCheckAvailabilityService;
        private readonly Mock<IUpdateStockService> _mockUpdateStockService;
        private readonly StockUC _stockUC;

        public StockUCTests()
        {
            _mockCheckAvailabilityService = new Mock<ICheckAvailabilityService>();
            _mockUpdateStockService = new Mock<IUpdateStockService>();
            _stockUC = new StockUC(_mockCheckAvailabilityService.Object, _mockUpdateStockService.Object);
        }

        [Fact]
        public async Task GetRecipeById_ShouldReturnRecipe_WhenRecipeExists()
        {
            // Arrange
            int recipeId = 1;
            int orderId = 100;
            var expectedRecipe = new StockDto
            {
                RecipeId = recipeId,
                OrderId = orderId,
                Available = true,
                Ingredients = new List<IngredientDto>
                {
                    new IngredientDto { IdIngredient = 1, Quantity = 2 }
                }
            };

            _mockCheckAvailabilityService
                .Setup(service => service.GetRecipeById(recipeId, orderId))
                .ReturnsAsync(expectedRecipe);

            // Act
            var result = await _stockUC.GetRecipeById(recipeId, orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedRecipe.RecipeId, result.RecipeId);
            Assert.Equal(expectedRecipe.OrderId, result.OrderId);
            Assert.Equal(expectedRecipe.Available, result.Available);
            Assert.Equal(expectedRecipe.Ingredients.Count, result.Ingredients.Count);
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

            _mockUpdateStockService
                .Setup(service => service.UpdateStockAsync(ingredients))
                .ReturnsAsync(true);

            // Act
            var result = await _stockUC.UpdateStockAsync(ingredients);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateStockAsync_ShouldReturnFalse_WhenStockUpdateFails()
        {
            // Arrange
            var ingredients = new List<IngredientDto>
            {
                new IngredientDto { IdIngredient = 1, Quantity = 5 },
                new IngredientDto { IdIngredient = 2, Quantity = 3 }
            };

            _mockUpdateStockService
                .Setup(service => service.UpdateStockAsync(ingredients))
                .ReturnsAsync(false);

            // Act
            var result = await _stockUC.UpdateStockAsync(ingredients);

            // Assert
            Assert.False(result);
        }
    }
}
