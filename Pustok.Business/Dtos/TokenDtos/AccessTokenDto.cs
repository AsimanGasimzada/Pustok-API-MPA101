namespace Pustok.Business.Dtos;

public class AccessTokenDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiredDate { get; set; }
}
