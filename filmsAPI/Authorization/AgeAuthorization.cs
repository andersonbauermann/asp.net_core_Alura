using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace filmsAPI.Authorization;

public class AgeAuthorization : AuthorizationHandler<MinAge>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinAge requirement)
    {
        var dateOfBirthClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);

        if (dateOfBirthClaim is null)
            return Task.CompletedTask;

        var dateOfBirth = Convert.ToDateTime(dateOfBirthClaim.Value);   

        var userAge = DateTime.Today.Year - dateOfBirth.Year;

        if (dateOfBirth > DateTime.Today.AddYears(-userAge))
            userAge--;

        if (userAge >= requirement.Age)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
