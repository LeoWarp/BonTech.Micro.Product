namespace BonTech.Product.Api;

public class JwtSettings
{
    public const string DefaultSection = "Jwt";
    public string Authority { get; set; }
    
    public string Audience { get; set; }
    
    public string Issuer { get; set; }
    
    public string JwtKey { get; set; }
    
    public int Lifetime { get; set; }
}