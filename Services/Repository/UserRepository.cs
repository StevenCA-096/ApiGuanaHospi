using AutoMapper;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class UserRepository : GenericRepository<Usuario>, IUserRepository
    {
        private readonly GuanaHospiContext _context;
        private readonly IMapper _mapper;
        public UserRepository(GuanaHospiContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<object> Login(string email, string password)
        {
            Usuario userFound = _context.usuario.Include(user => user.rol).Where(user => user.Correo == email).FirstOrDefault();

            SecurityToken token = null;
            var tokenHandler = new JwtSecurityTokenHandler();

            if (userFound != null)
            {
                string result = string.Empty;
                byte[] encryted = System.Text.Encoding.Unicode.GetBytes(password);
                result = Convert.ToBase64String(encryted);
                if (userFound.Contra == password)
                {
                    var tokenKey = Encoding.UTF8.GetBytes("the secret key that is going to be used"); //IConfiguration["JWT:Key"]
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                      {
                       new Claim(ClaimTypes.Email, userFound.Correo),
                       new Claim(ClaimTypes.Role,userFound.rol.Nombre.ToString())
                      }),
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                    };
                    token = tokenHandler.CreateToken(tokenDescriptor);
                    return tokenHandler.WriteToken(token);
                }
                else
                {
                    return "Wrong password";
                }

            }
            else
            {
                return "User not found";
            }

        }

        public async Task<object> getLoginInfo(string email, string password)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(password);
            result = Convert.ToBase64String(encryted);

            Usuario user = _context.usuario.Include(u => u.rol)
                .Where(user => user.Correo == email && user.Contra == result)
                .FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public string getChangePasswordToken(string email)
        {
            Usuario user = _context.usuario.Where(u => u.Correo == email).FirstOrDefault();
            SecurityToken token = null;

            var tokenHandler = new JwtSecurityTokenHandler();
            if (user != null)
            {
                var tokenKey = Encoding.UTF8.GetBytes("the secret key that is going to be used"); //IConfiguration["JWT:Key"]
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                  {
                       new Claim(ClaimTypes.Email, user.Correo),
                       new Claim(ClaimTypes.Role,"Restore")
                  }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            else
            {
                return null;
            }
        }

        //public Task<User> getUserById(int id)
        //{
        //    User user = _context.users.Include(u => u.role)
        //        .Include(user => user.costumer).ThenInclude(c => c.costumersContacts)
        //        .Include(u => u.employee)
        //        .Where(user => user.Id == id)
        //        .FirstOrDefault();
        //    if (user != null)
        //    {
        //        return Task.FromResult(user);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public string ChangePassword(string email, string newPassword)
        {
            Usuario user = _context.usuario.Where(u => u.Correo == email).FirstOrDefault();
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(newPassword);
            result = Convert.ToBase64String(encryted);
            user.Contra = result;
            _context.SaveChanges();

            return email;
        }

        //public Task<object> CreateUser(UserDTO newUser)
        //{
        //    return null;
        //}

        //Task<Usuario> IUserRepository.getUserById(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
