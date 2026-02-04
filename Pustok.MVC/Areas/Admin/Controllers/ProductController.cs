using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens.Experimental;
using Pustok.Business.Dtos;

namespace Pustok.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{

    private readonly HttpClient _client;

    public ProductController(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("ApiClient");
    }
    public async Task<IActionResult> Index()
    {


        var response = await _client.GetAsync("api/Products");


        if (!response.IsSuccessStatusCode)
        {

            return Json(response);
        }


        var result = await response.Content.ReadFromJsonAsync<ResultDto<List<ProductGetDto>>>() ?? new();




        return View(result.Data);
    }


    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var formContent = CreateFormContent(dto);
        var result = await _client.PostAsync("api/Products", formContent);

        if (!result.IsSuccessStatusCode)
        {
            var content = await result.Content.ReadFromJsonAsync<ResultDto>() ?? new("Something was wrong");
            ModelState.AddModelError("", content.Message);
            return View(dto);
        }

        return RedirectToAction("Index");
    }

    private MultipartFormDataContent CreateFormContent(ProductCreateDto dto)
    {
        var content = new MultipartFormDataContent();

        content.Add(new StringContent(dto.Name), "Name");
        content.Add(new StringContent(dto.Description), "Description");
        content.Add(new StringContent(dto.Price.ToString()), "Price");
        content.Add(new StringContent(dto.CategoryId.ToString()), "CategoryId");

        if (dto.Image != null)
        {
            var fileContent = new StreamContent(dto.Image.OpenReadStream());
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(dto.Image.ContentType);
            content.Add(fileContent, "Image", dto.Image.FileName);
        }

        return content;
    }
}
