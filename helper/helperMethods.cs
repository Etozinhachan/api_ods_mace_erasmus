using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace api_ods_mace_erasmus.helper;

public static class HelperMethods
{

    #region ArrayHelpers

    public static bool arrayContains(string[] responsePool, string response)
    {
        foreach (string item in responsePool)
        {
            if (item.ToLower().Contains(response.ToLower()))
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    #region Jwt thingies

    public static JwtSecurityToken ConvertJwtStringToJwtSecurityToken(string? jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);

        return token;
    }

    public static DecodedToken DecodeJwt(JwtSecurityToken token)
    {
        var keyId = token.Header.Kid;
        var audience = token.Audiences.ToList();
        var claims = token.Claims.Select(claim => (claim.Type, claim.Value)).ToList();
        return new DecodedToken(
            keyId,
            token.Issuer,
            audience,
            claims,
            token.ValidTo,
            token.SignatureAlgorithm,
            token.RawData,
            token.Subject,
            token.ValidFrom,
            token.EncodedHeader,
            token.EncodedPayload
        );
    }

    public static JwtSecurityToken decodeToken(/*string JwtTokenString, string notEncodedKey, */IConfiguration _config, HttpContext httpContext)
    {

        var notEncodedKey = _config["JwtSettings:Key"]!;
        var JwtTokenString = httpContext.Request.Headers.Authorization;
        JwtTokenString = JwtTokenString.ToString().Substring("Bearer ".Length);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(notEncodedKey);
        //token = token.ToString().Substring("Bearar ".Length);
        tokenHandler.ValidateToken(JwtTokenString, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        return jwtToken;
    }

    public record DecodedToken(
        string KeyId,
        string Issuer,
        List<string> Audience,
        List<(string Type, string Value)> Claims,
        DateTime Expiration,
        string SigningAlgorithm,
        string RawData,
        string Subject,
        DateTime ValidFrom,
        string Header,
        string Payload
    );
    #endregion

    #region Image Verification

    public static async Task<bool> isImageLinkValid(List<string> image_uris)
    {

        foreach (var image_uri in image_uris)
        {
            if (!Uri.IsWellFormedUriString(image_uri, UriKind.Absolute))
            {
                return false;
            }

            if (!await doesLinkPointToAnImage(image_uri))
            {
                return false;
            }
        }

        return true;

    }

    public static async Task<bool> doesLinkPointToAnImage(string uri)
    {
        using (var httpClient = new HttpClient())
        {
            try
            {
                httpClient.Timeout = TimeSpan.FromSeconds(5);
                var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri));
                if (response.IsSuccessStatusCode &&
                   response.Content.Headers.ContentType.MediaType.StartsWith("image/"))
                {
                    return true;
                }
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
        return false;
    }

    #endregion
}