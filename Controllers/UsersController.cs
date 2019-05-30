using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CommercantsAPI.Exceptions;
using CommercantsAPI.Models.Users;
using CommercantsAPI.Repository;
using CommercantsAPI.Util;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CommercantsAPI.Controllers
{
    [Route("/api/user")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(IRepositoryWrapper repositoryWrapper, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._mapper = mapper;
            this._appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserDto userParam)
        {
            var user = _repositoryWrapper.User.Authenticate(userParam.Mail, userParam.password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Id = user.Id,
                Mail = user.Mail,
                Name = user.Name,
                Surname = user.Surname,
                Token = tokenString
            });
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<User> users = this._repositoryWrapper.User.FindAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            User user = this._repositoryWrapper.User.Find(id);
            if(user == null)
            {
                return NotFound("L'utilisateur n'existe pas");
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto userDTO)
        {
            var user = _mapper.Map<User>(userDTO);

            try
            {
                _repositoryWrapper.User.Create(user, userDTO.password);
                return Ok();
            }
            catch (AppException exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                _repositoryWrapper.User.Update(user, userDto.password);
                return Ok();
            }
            catch (AppException exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            User user = this._repositoryWrapper.User.Find(id);
            if(user == null)
            {
                return NotFound("L'utilisateur n'existe pas.");
            }
            this._repositoryWrapper.User.Delete(user);
            return NoContent();
        }
    }
}