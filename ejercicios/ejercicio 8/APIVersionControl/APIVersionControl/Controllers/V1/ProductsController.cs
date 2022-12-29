using APIVersionControl.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APIVersionControl.Controllers.V1
{
	[ApiVersion("1.0")]
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

		[MapToApiVersion("1.0")]
		[HttpGet(Name = "GetProductsData")]
		public async Task<IActionResult> GetProducts()
		{
			var response = await _httpClient.GetStreamAsync(ApiTestURL);
			var productsData = await JsonSerializer.DeserializeAsync
												<List<ProductsWithRating>>(response);

			return Ok(productsData);
		}
	}
}
