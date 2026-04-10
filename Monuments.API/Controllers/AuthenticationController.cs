using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Monuments.API.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Monuments.API.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController(IConfiguration configuration) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration ??
        throw new ArgumentNullException(nameof(configuration));

    
    [HttpPost("authenticate")]
    public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
    {
        //Step 1: Validate the username/password
        var user = ValidateUserCredentials(
            authenticationRequestBody.UserName,
            authenticationRequestBody.Password);

        //if no user is returned, it means credentials are not valid
        if (user is null)
        {
            return Unauthorized();
        }
        else
        {
            //Step 2: Create a token
            //-(a)- Use the configuration instance to get the secret stored in appsttings.Development
            //and convert it to an array of bytes;
            SymmetricSecurityKey securityKey = new(Encoding.ASCII
                .GetBytes(_configuration["Authentication:SecretForKey"]));

            //-(b)- Create signing credentials
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claimsForToken = [];
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("given_name", user.FirstName));
            claimsForToken.Add(new Claim("family_name", user.LastName));
            claimsForToken.Add(new Claim("city", user.City));

            //-(c)- Create a JwtSecurityToken
            JwtSecurityToken jwtsecurityToken = new(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,//Indicates the start of token validity
                DateTime.UtcNow.AddHours(1),//Indicates the end of token validity
                signingCredentials);

            string tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtsecurityToken);

            return Ok(tokenToReturn);
        }

    }

    /// <summary>
    /// Checks the username/password against what's stored in the database
    /// </summary>
    /// <param name="userName">Username of consumer of the api</param>
    /// <param name="password">Password of consumer of the api</param>
    /// <returns>returns an instance of the validated user</returns>
    private MonumentUser ValidateUserCredentials(string userName, string password)
    {
        //Check the username/password against what's stored in the database
        //If they are valid, return a new MonumentUser instance

        return new MonumentUser(1,
            userName ?? "",
            "me",
            "us",
            "Bauchi");

    }





    //We wont use this outside of this class, so we can define it within this class
    public class AuthenticationRequestBody
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    //We wont use this outside of this class, so we can define it within this class
    public class MonumentUser
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }

        public MonumentUser(int userId, string userName,
            string firstName, string lastName, string city)
        {
            UserId = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            City = city;
        }
    }

}
