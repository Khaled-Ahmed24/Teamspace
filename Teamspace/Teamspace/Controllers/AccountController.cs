using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;
using Teamspace.Repositories;
using Teamspace.SpaghettiModels;
using BCrypt.Net;

namespace Teamspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        public AccountRepo _accountRepo;
        private readonly IConfiguration config;

        public AccountController(AccountRepo accountRepo, IConfiguration config)
        {
            _accountRepo = accountRepo;
            this.config = config;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByRole(int role)
        {
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var roleClaim = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            Console.WriteLine(id);
            if (role == 3)
            {
                var students = await _accountRepo.GetAllStudents();
                return Ok(new { id, email, roleClaim, students });
            }
            else if(role < 3)
            {
                var staffs = await _accountRepo.GetAllStaffs(role);
                return Ok(new { id, email, roleClaim, staffs });
            }
            return BadRequest("Invalid role please ensure you select a valid role :)");
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int role, int id)
        {
            if (role == 3)
            {
                var student = await _accountRepo.GetStudentById(id);
                if (student != null)
                    return Ok(student);
                return NotFound("Student not found :(");
            }
            else if(role < 3)
            {
                var staff = await _accountRepo.GetStaffById(id);
                if (staff != null)
                    return Ok(staff);
                return NotFound("Staff not found :(");
            }
            return BadRequest("Invalid role please ensure you select a valid role :)");
        }


        [HttpPost("[action]")]

        public async Task<IActionResult> AddAccount([FromQuery] int role, [FromForm] Account account)
        {
            var ok = await _accountRepo.Add(role, account);  
            if(ok)
                return Ok();
            return BadRequest("There is a problem occured when adding this account ensure you selected a valid role and try again ^_^");
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> AddByExcel([FromForm] Excel file)
        {
            await _accountRepo.AddByExcel(file);
            await _accountRepo.SaveChanges();
            return Ok();
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromQuery] int role, [FromQuery] int id, [FromForm] Account account)
        {
            await _accountRepo.Update(role, id, account);
            await _accountRepo.SaveChanges();
            return Ok();
        }


        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(int role, int id)
        {
            await _accountRepo.Delete(role, id);
            await _accountRepo.SaveChanges();
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUser UserFromRequest)
        {
            var user = await _accountRepo.GetByEmail(UserFromRequest.Email);
            if (user != null)
            {
                /*BCrypt.Net.BCrypt.Verify(UserFromRequest.Password, user.Password)*/
                if (UserFromRequest.Password == user.Password)
                {
                    // Claims
                    List<Claim> UserClaims = new List<Claim>();
                    UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    UserClaims.Add(new Claim(ClaimTypes.Email, user.Email));
                    UserClaims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
                    UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    // Key
                    var SignInKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecritKey"]));

                    // Algorithm
                    SigningCredentials SigningCred =
                            new SigningCredentials(SignInKey, SecurityAlgorithms.HmacSha256);

                    // Predifined Claims
                    JwtSecurityToken token = new JwtSecurityToken(
                        issuer: config["JWT:Issuer"],
                        audience: config["JWT:Audience"],
                        expires: DateTime.Now.AddMinutes(30),
                        claims: UserClaims,
                        signingCredentials: SigningCred
                    );
                    var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    Console.WriteLine(id);
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = DateTime.Now.AddMinutes(30)
                    });
                }
            }
            return Unauthorized("Invalid email or password");
        }
    }
}