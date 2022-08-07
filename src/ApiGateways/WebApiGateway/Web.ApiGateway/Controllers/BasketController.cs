﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ApiGateway.Models.Basket;
using Web.ApiGateway.Services.Interfaces;

namespace Web.ApiGateway.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class BasketController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly IBasketService _basketService;

    public BasketController(ICatalogService catalogService, IBasketService basketService)
    {
        _catalogService = catalogService;
        _basketService = basketService;
    }

    [HttpPost]
    [Route("items")]
    public async Task<IActionResult> AddBasketItemAsync([FromBody] AddBasketItemRequest request)
    {
        if (request == null || request.Quantity == 0)
        {
            return BadRequest("Invalid Payload");
        }

        var item = await _catalogService.GetCatalogItemAsync(request.CatalogItemId);

        var currentBasket = await _basketService.GetById(request.BasketId);

        var product = currentBasket.Items.SingleOrDefault(i => i.ProductId == item.Id);

        if (product != null)
        {
            product.Quantity += request.Quantity;
        }
        else
        {
            currentBasket.Items.Add(new BasketDataItem()
            {
                UnitPrice = item.Price,
                Quantity = request.Quantity,
                Id = Guid.NewGuid().ToString(),
                PictureUrl = item.PictureUrl,
                ProductId = item.Id,
                ProductName = item.Name
            });
        }

        await _basketService.UpdateAsync(currentBasket);

        return Ok();
    }
}