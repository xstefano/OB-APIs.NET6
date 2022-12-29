using APIVersionControl.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APIVersionControl.Controllers.V2
{
	[ApiVersion("2.0")]
	[Route("api/v{version:apiVersion}[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private const string ApiTestURL = "https://fakestoreapi.com/products";
		private readonly HttpClient _httpClient;

		public ProductsController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		[MapToApiVersion("2.0")]
		[HttpGet(Name = "GetProductsProducts")]
		public async Task<IActionResult> GetProductsDataASync()
		{
			var response = await _httpClient.GetStreamAsync(ApiTestURL);
			var productsData = await JsonSerializer.DeserializeAsync
												<List<ProductsWithoutRating>>(response);
			return Ok(productsData);
		}
	}
}
