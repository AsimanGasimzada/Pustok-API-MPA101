using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pustok.Business.Dtos;
using Pustok.Business.Dtos.TokenDtos;

namespace Pustok.MVC.Controllers;

public class AccountController : Controller
{
    private readonly HttpClient _client;

    public AccountController(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("ApiClient");
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        // POST to API endpoint
        var response = await _client.PostAsJsonAsync("api/Auth/Login", dto);

        if (!response.IsSuccessStatusCode)
        {
            // try to show server error message (if any)
            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, string.IsNullOrWhiteSpace(error) ? response.ReasonPhrase! : error);
            return View(dto);
        }

        var tokenResult = await response.Content.ReadFromJsonAsync<ResultDto<AccessTokenDto>>();
        if (tokenResult is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid response from authentication server.");
            return View(dto);
        }

        // Example: store access token as an HttpOnly cookie
        Response.Cookies.Append("AccessToken", tokenResult.Data!.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = tokenResult.Data.ExpiredDate
        });
        // Example: store access token as an HttpOnly cookie
        Response.Cookies.Append("RefreshToken", tokenResult.Data!.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = tokenResult.Data.RefreshTokenExpiredDate
        });



        //return Ok(tokenResult);

        // Redirect on successful login
        return RedirectToAction("Index", "Home");
    }
}
