using Azure;
using Microsoft.AspNetCore.Http;
using Pustok.Business.Dtos;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pustok.MVC.Handlers;

public class AuthTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CookieName = "AccessToken";
    private const string RefreshTokenCookieName = "RefreshToken";

    public AuthTokenHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {

        var ctx = _httpContextAccessor.HttpContext;
        var tokenValue = ctx?.Request?.Cookies[CookieName];

    restart:

        if (!string.IsNullOrWhiteSpace(tokenValue))
        {
            var raw = tokenValue.StartsWith("Bearer ", System.StringComparison.OrdinalIgnoreCase)
                ? tokenValue.Substring("Bearer ".Length)
                : tokenValue;

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", raw);
        }

        var response = await base.SendAsync(request, cancellationToken);


        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var refreshTokenValue = ctx?.Request?.Cookies[RefreshTokenCookieName];


                if (!string.IsNullOrWhiteSpace(refreshTokenValue))
                {
                    HttpRequestMessage httpRequestMessage = new(HttpMethod.Post, "https://localhost:44385/api/Auth/RefreshToken");


                    httpRequestMessage.Content =
                        new StringContent($"\"{refreshTokenValue}\"", Encoding.UTF8, "application/json");


                    var refreshTokenResponse = await base.SendAsync(httpRequestMessage, cancellationToken);


                    if (refreshTokenResponse.IsSuccessStatusCode)
                    {
                        var newAccessTokenResult = await refreshTokenResponse.Content.ReadFromJsonAsync<ResultDto<AccessTokenDto>>();



                        ctx!.Response.Cookies.Append("AccessToken", newAccessTokenResult!.Data!.Token, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            Expires = newAccessTokenResult!.Data.ExpiredDate
                        });

                        ctx!.Response.Cookies.Append("RefreshToken", newAccessTokenResult!.Data!.RefreshToken, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            Expires = newAccessTokenResult!.Data.RefreshTokenExpiredDate
                        });
                        tokenValue = newAccessTokenResult.Data.Token;


                        goto restart;

                    }
                    else
                    {
                        ctx!.Response.Redirect("/Account/Login");
                    }

                }
                else
                {
                    ctx!.Response.Redirect("/Account/Login");
                }


            }

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                ctx!.Response.Redirect("/Home/AccessDenied");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                ctx!.Response.Redirect("/Home/Error");
            }
        }

        return response;
    }
}
