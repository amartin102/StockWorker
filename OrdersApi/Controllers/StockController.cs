//using Application.Dto;
//using Application.Interface;
//using Application.Service;
using Application.Dto;
using Application.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StockApi.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ICheckAvailabilityService _checkAvailabilityService;
        private readonly IUpdateStockService _updateStockService;

        public StockController(ICheckAvailabilityService validateAvailabilityService, IUpdateStockService updateStockService )
        {
            _checkAvailabilityService = validateAvailabilityService;
            _updateStockService = updateStockService;
        }

        [HttpGet]
        [Route("CheckAvailability")]
        public async Task<ActionResult<bool>> CheckAvailability(int recipeId)
        {
            try
            {
                var result = await _checkAvailabilityService.GetRecipeById(recipeId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
           
        }
               
        [HttpPut]
        [Route("UpdateStock")]
        public async Task<ActionResult<bool>> UpdateStock(IngredientDto ingredient)
        {
            try
            {    
                var result = await _updateStockService.UpdateStockAsync(ingredient);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

    }
}
