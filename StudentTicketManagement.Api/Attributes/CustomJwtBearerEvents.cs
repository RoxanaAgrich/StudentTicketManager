using Application.Abtractions;
using Application.Services.V1.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace StudentTicketManagement.Api.Attributes;

public class CustomJwtBearerEvents : JwtBearerEvents
{
    private readonly ICacheService _cacheService;

    public CustomJwtBearerEvents(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public override async Task TokenValidated(TokenValidatedContext context)
    {
        if (context.SecurityToken is Microsoft.IdentityModel.JsonWebTokens.JsonWebToken jsonWebToken)
        {
            var requestToken = jsonWebToken.EncodedToken;

            var emailKey = jsonWebToken.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value;
            var authenticated = await _cacheService.GetAsync<Response.Authenticated>(emailKey);

            if (authenticated is null || authenticated.AccessToken != requestToken)
            {
                context.Response.Headers.Add("IS-TOKEN-REVOKED", "true");
                context.Fail("Authentication fail. Token has been revoked!");
            }
        }
        else
        {
            context.Fail("Authentication fail.");
        }
    }
}
