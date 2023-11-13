using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Services.IRepository;
using System.Data;

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        [HttpGet("GetchangePasswordToken")]
        public string Get(string email)
        {
            return _userRepository.getChangePasswordToken(email);
        }

        [HttpGet("Login")]
        public Task<object> Login(string email, string password)
        {
            return _userRepository.Login(email, password);
        }
        [HttpGet("getLoginInfo")]

        public async Task<object> getLoginInfo(string email, string password)
        {
            return await _userRepository.getLoginInfo(email, password);
        }

        [HttpPut("ChangePassword")]
        public string Get(string email, string newPassword)
        {
            return _userRepository.ChangePassword(email, newPassword);
        }
    }
}
