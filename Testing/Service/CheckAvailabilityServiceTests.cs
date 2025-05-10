using Application.Dto;
using Application.Service;
using Domain.Models;
using Moq;
using Repository.Interface;
using Xunit;

public class CheckAvailabilityServiceTests
{
    private readonly Mock<IRecipeRepository> _recipeRepositoryMock;
    private readonly Mock<IIngredientRepository> _ingredientRepositoryMock;
    private readonly CheckAvailabilityService _checkAvailabilityService;

    public CheckAvailabilityServiceTests()
    {
        _recipeRepositoryMock = new Mock<IRecipeRepository>();
        _ingredientRepositoryMock = new Mock<IIngredientRepository>();
        _checkAvailabilityService = new CheckAvailabilityService(_recipeRepositoryMock.Object, _ingredientRepositoryMock.Object);
    }

    [Fact]
    public async Task GetRecipeById_ShouldReturnAvailableStock_WhenIngredientsAreSufficient()
    {
        // Arrange
        var recipeId = 1;
        var orderId = 100;

        var recipe = new Recipe
        {
            IdRecipe = recipeId,
            RecipeIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { IdIngredient = 1, Quantity = 5 }
            }
        };

        var ingredients = new List<Ingredient>
        {
            new Ingredient { IdIngredient = 1, Quantity = 10 }
        };

        _recipeRepositoryMock
            .Setup(repo => repo.GetRecipeById(recipeId))
            .ReturnsAsync(recipe);

        _ingredientRepositoryMock
            .Setup(repo => repo.GetAllIngredients())
            .ReturnsAsync(ingredients);

        // Act
        var result = await _checkAvailabilityService.GetRecipeById(recipeId, orderId);

        // Assert
        Assert.True(result.Available);
        Assert.Single(result.Ingredients);
        Assert.Equal(1, result.Ingredients[0].IdIngredient);
    }

    [Fact]
    public async Task GetRecipeById_ShouldReturnUnavailableStock_WhenIngredientsAreInsufficient()
    {
        // Arrange
        var recipeId = 1;
        var orderId = 100;

        var recipe = new Recipe
        {
            IdRecipe = recipeId,
            RecipeIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { IdIngredient = 1, Quantity = 15 }
            }
        };

        var ingredients = new List<Ingredient>
        {
            new Ingredient { IdIngredient = 1, Quantity = 10 }
        };

        _recipeRepositoryMock
            .Setup(repo => repo.GetRecipeById(recipeId))
            .ReturnsAsync(recipe);

        _ingredientRepositoryMock
            .Setup(repo => repo.GetAllIngredients())
            .ReturnsAsync(ingredients);

        // Act
        var result = await _checkAvailabilityService.GetRecipeById(recipeId, orderId);

        // Assert
        Assert.False(result.Available);
        Assert.Empty(result.Ingredients);
    }
}
