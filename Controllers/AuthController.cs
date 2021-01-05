using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MarqueesAssistant.API.Data;
using MarqueesAssistant.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.AspNetCore.Authorization;

namespace MarqueesAssistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthRepo _repo;
        private readonly IConfiguration _config;


        public AuthController(IAuthRepo repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;

        }

        [Authorize(Roles = "admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(WorkerRegisterDto workerRegisterDto)
        {
            workerRegisterDto.Login = workerRegisterDto.Login.ToLower();

            if (await _repo.WorkerIsExist(workerRegisterDto.Login))
            {
                return BadRequest("Taki użytkownik już istnieje");
            }

            var workerToCreate = new Worker
            {
                Rank = workerRegisterDto.Rank,
                FirstName = workerRegisterDto.FirstName,
                LastName = workerRegisterDto.LastName,
                Login = workerRegisterDto.Login
            };

            var createdWorker = await _repo.Register(workerToCreate, workerRegisterDto.Password);

            return StatusCode(201);

        }

        [HttpPut("editWorker")]
        public async Task<IActionResult> EditWorker(WorkerEditDto workerEditDto)
        {
            workerEditDto.Login = workerEditDto.Login.ToLower();

            if (await _repo.WorkerIsExist(workerEditDto.Login))
            {
                if (!await _repo.CanEditWorker(workerEditDto))
                {
                    return BadRequest("Użytkownik o podanym nicku istnieje");
                }

            }

            var workerToCreate = new Worker
            {
                Id = workerEditDto.Id,
                Rank = workerEditDto.Rank,
                FirstName = workerEditDto.FirstName,
                LastName = workerEditDto.LastName,
                Login = workerEditDto.Login
            };

            var createdWorker = await _repo.EditWorker(workerToCreate, workerEditDto.Password);

            return StatusCode(201);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(WorkerLoginDto workerLoginDto)
        {
            var workerFromRepo = await _repo.Login(workerLoginDto.Login.ToLower(), workerLoginDto.Password);

            if (workerFromRepo == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, workerFromRepo.Id.ToString()), //Tutaj w tokenie zapisane jest ID
                new Claim(ClaimTypes.Name, workerFromRepo.Login), // Tutaj w tokenie jest zapisany login
                new Claim(ClaimTypes.Role, workerFromRepo.Rank)//Tutaj dodaj stopien pracownika
                
                
            };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(8), //Tu ustawia sie czas jak dlugo ma byc token przewaznie duzo krocej
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}