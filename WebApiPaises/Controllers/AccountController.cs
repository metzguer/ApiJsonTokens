using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApiPaises.Entities;

namespace WebApiPaises.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _cofiguration;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cofiguration = configuration;
        }
        
        // GET: api/Account/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Account
        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserInfo model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User() { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    return BuildToken(model);
                }
                else {
                    return BadRequest("Verifique los datos ingresados");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return Ok(user) ;
        }
        //Post: api/login
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserInfo user) {
            if (ModelState.IsValid) {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, isPersistent:false, lockoutOnFailure:false);
                if (result.Succeeded)
                {
                    return BuildToken(user);
                }
                else {
                    ModelState.AddModelError(string.Empty, "Fallo en la autenticacion");
                    return BadRequest(ModelState);
                }
            } else {
                return BadRequest(ModelState);
            }
        }
        // PUT: api/Account/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        private IActionResult BuildToken(UserInfo user) {
            var claims = new[] {
                 new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                 new Claim("mi valor", "lo que yo quiera"),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            //key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cofiguration["key_toekn_json"]));
            //hash
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expirations = DateTime.UtcNow.AddDays(10);

            JwtSecurityToken tokenJs = new JwtSecurityToken(
                issuer: "misitioweb.com",
                audience : "misitioweb.com",
                claims : claims,
                expires : expirations,
                signingCredentials: creds
                );
            return Ok(
                new {
                    token = new JwtSecurityTokenHandler().WriteToken(tokenJs),
                    expiration = expirations
                    }
                );
        }
    }
}
