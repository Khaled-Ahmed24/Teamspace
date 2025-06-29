﻿using Microsoft.AspNetCore.Authorization;
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
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Teamspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        public AccountRepo _accountRepo;
        private readonly IConfiguration config;

        // for test
        private readonly AppDbContext _db;

        public AccountController(AccountRepo accountRepo, IConfiguration config, AppDbContext db)
        {
            _accountRepo = accountRepo;
            this.config = config;
            _db = db;
        }



        [HttpGet("[action]")]
        [Authorize]
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
        [Authorize]
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
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetAllStudentsByYear(int year)
        {
            return Ok(await _accountRepo.GetAllStudentsByYear(year));
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetAllStudentsByDepartment(string department)
        {
            return Ok(await _accountRepo.GetAllStudentsByDepartment(department));
        }


        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAccount([FromQuery] int role, [FromForm] Account account)
        {
            var ok = await _accountRepo.Add(role, account);  
            if(ok == "Ok")
                return Ok("Account added successfully");
            return BadRequest(ok);
        }

       


        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddByExcel([FromForm] Excel file)
        {
            var errors = await _accountRepo.AddByExcel(file);
            if (errors == null || errors.Count == 0) return Ok("Data added successfully");
            else return BadRequest(errors);
        }


        [HttpPut("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromQuery] int role, [FromQuery] int id, [FromForm] Account account)
        {
            var ok = await _accountRepo.Update(role, id, account);
            if(ok == "Ok")
                return Ok("Account updated successfully");
            else return BadRequest(ok);
        }


        [HttpDelete("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int role, int id)
        {
            var ok = await _accountRepo.Delete(role, id);
            if (ok == "Ok")
                return Ok("Account deleted successfully");
            return BadRequest(ok);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUser UserFromRequest)
        {
            var user = await _accountRepo.GetByEmail(UserFromRequest.Email);
            if (user != null)
            {
                if (user.Password == UserFromRequest.Password)
                {
                    // Claims
                    List<Claim> UserClaims = new List<Claim>();
                    UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    UserClaims.Add(new Claim(ClaimTypes.Name, user.Name));
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