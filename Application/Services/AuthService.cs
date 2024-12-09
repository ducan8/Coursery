using Application.Handle.HandleEmail;
using Application.IServices;
using Application.Payloads.RequestModels.DataUser;
using Application.Payloads.Response;
using Application.Payloads.ResponseModels.DataUser;
using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using Domain.Entities;
using Domain.IRepositories;
using Domain.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        #region private field
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<ConfirmEmail> _baseConfirmEmailRepository;
        private readonly IBaseRepository<Permission> _basePermissionRepository;
        private readonly IBaseRepository<Role> _baseRoleRepository;
        private readonly IBaseRepository<RefreshToken> _baseRefreshTokenRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        #endregion


        public AuthService(IMapper mapper,
                            IConfiguration configuration,
                            IUserRepository userRepository,
                            IEmailService emailService,
                            IHttpContextAccessor contextAccessor,
                            IBaseRepository<User> baseUserRepository,
                            IBaseRepository<ConfirmEmail> baseConfirmEmailRepository,
                            IBaseRepository<Permission> basePermissionRepository,
                            IBaseRepository<Role> baseRoleRepository,
                            IBaseRepository<RefreshToken> baseRefreshTokenRepository
            )
        {
            _mapper = mapper;
            _configuration = configuration;
            _userRepository = userRepository;
            _emailService = emailService;
            _baseUserRepository = baseUserRepository;
            _baseConfirmEmailRepository = baseConfirmEmailRepository;
            _basePermissionRepository = basePermissionRepository;
            _baseRoleRepository = baseRoleRepository;
            _baseRefreshTokenRepository = baseRefreshTokenRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<ResponseObject<string>> ConfirmRegisterAccount(string confirmCode)
        {
            try
            {
                var code = await _baseConfirmEmailRepository.GetAsync(x => x.ConfirmCode.Equals(confirmCode));

                if (code == null) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Your code is invalid. Please try again",
                    Data = null
                };

                if (code.ExpiryTime < DateTime.Now) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Your code has expired. Please try again",
                    Data = null
                };

                var user = await _baseUserRepository.GetByIdAsync(x => x.Id == code.UserId);

                if (user == null) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Something is invalid. Please register again",
                    Data = null
                };

                user.UserStatus = Domain.Enumerates.ConstantEnums.UserStatus.Activated;
                code.IsConfirmed = true;
                await _baseConfirmEmailRepository.UpdateAsync(code);
                await _baseUserRepository.UpdateAsync(user);

                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Email confirmation successfull. Now you can use your account to log in. Thanks",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<DataResponseUser_Login>> Login(DataRequestUser_Login login)
        {
            var user = await _baseUserRepository.GetAsync(x => x.UserName.Equals(login.UserName));
            if (user == null) return new ResponseObject<DataResponseUser_Login>
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = "Wrong Username or Password",
                Data = null
            };

            if (user.UserStatus.ToString().Equals(Domain.Enumerates.ConstantEnums.UserStatus.UnActived.ToString()))
            {
                return new ResponseObject<DataResponseUser_Login>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You hasn't confirmed your email yet.",
                    Data = null
                };
            }

            bool checkPass = BCryptNet.Verify(login.Password, user.Password);
            if (!checkPass) return new ResponseObject<DataResponseUser_Login>
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = "Wrong Username or Password",
                Data = null
            };


            //Don't return refresh token with access token, return rf to client using cookie
            var refreshToken = GenerateRefreshToken();
            //_ = int.TryParse(_configuration["JWT:RefreshTokenValidaty"], out int refreshTokenValidity);

            RefreshToken refresh = new RefreshToken
            {
                ExpiryTime = DateTime.UtcNow.AddHours(7),
                UserId = user.Id,
                Token = refreshToken
            };
            refresh = await _baseRefreshTokenRepository.CreateAsync(refresh);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                Expires = DateTime.UtcNow.AddHours(7),
            };
            _contextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);


            return new ResponseObject<DataResponseUser_Login>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Login successfully",
                Data = new DataResponseUser_Login
                {
                    AccessToken = await GetJwtTokenAsync(user)
                }
            };
        }

        public async Task<ResponseObject<DataResponseUser>> Register(DataRequestUser_Register register)
        {
            try
            {
                if (!ValidationInput.IsValidEmail(register.Email))
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Email is invalid",
                        Data = null
                    };
                }
                if (!ValidationInput.IsValidPhoneNumber(register.PhoneNumber))
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Phone number is invalid",
                        Data = null
                    };
                }
                if (await _userRepository.GetUserByEmail(register.Email) != null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Email is exists",
                        Data = null
                    };
                }
                if (await _userRepository.GetUserByPhoneNumber(register.PhoneNumber) != null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Phone number is exists",
                        Data = null
                    };
                }
                if (await _userRepository.GetUserByUsername(register.UserName) != null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Username is exists",
                        Data = null
                    };
                }


                var user = _mapper.Map<User>(register);
                user.Avatar = GetLinkAvatarDefault();
                user.IsActive = true;
                user = await _baseUserRepository.CreateAsync(user);
                await _userRepository.AddRolesToUserAsync(user, new List<string> { "User" });


                ConfirmEmail confirmEmail = new ConfirmEmail
                {
                    ConfirmCode = GenerateCodeActive(),
                    ExpiryTime = DateTime.Now.AddMinutes(5),
                    IsConfirmed = false,
                    UserId = user.Id,
                };
                await _baseConfirmEmailRepository.CreateAsync(confirmEmail);


                var email = new EmailMessage(new List<string> { user.Email },
                                                        "Welcome to Coursery 🥑",
                                                        $"Your confirmation code: {confirmEmail.ConfirmCode}");
                _emailService.SendEmail(email);

                return new ResponseObject<DataResponseUser>
                {
                    StatusCode = StatusCodes.Status201Created,
                    Message = "New member registration successful, please check your email to get the confirmation code.",
                    Data = _mapper.Map<DataResponseUser>(user)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<DataResponseUser>> ChangePassword(Guid userId, DataRequestUser_ChangePassword changePassword)
        {
            try
            {
                var user = await _baseUserRepository.GetByIdAsync(x => x.Id == userId);
                bool checkPass = BCryptNet.Verify(changePassword.OldPassword, user.Password);
                if (!checkPass) return new ResponseObject<DataResponseUser>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Your old password is incorrect",
                    Data = null
                };

                if (!changePassword.NewPassword.Equals(changePassword.ConfirmPassword))
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "The passwords does not match",
                        Data = null
                    };
                }

                if (changePassword.NewPassword.Equals(changePassword.OldPassword))
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "The new passwords cannot be the same as the old password.",
                        Data = null
                    };
                }

                user.Password = BCryptNet.HashPassword(changePassword.NewPassword);
                user.UpdateTime = DateTime.Now;
                await _baseUserRepository.UpdateAsync(user);

                var listRefreshToken = await _baseRefreshTokenRepository.GetAllAsync(x => x.UserId == userId);
                if(listRefreshToken != null)
                {
                    foreach (var token in listRefreshToken)
                    {
                        await _baseRefreshTokenRepository.DeleteAsync(token.Id);
                    }
                }


                return new ResponseObject<DataResponseUser>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Change password successfully",
                    Data = _mapper.Map<DataResponseUser>(user)
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Internal Server Error",
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<string>> ForgotPassword(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(email);
                if (user == null) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Your account does not exists in our system.",
                    Data = null
                };
                var listConfirmCodes = await _baseConfirmEmailRepository.GetAllAsync(x => x.UserId == user.Id);
                if(listConfirmCodes.ToList().Count() > 0)
                {
                    foreach (var code in listConfirmCodes)
                    {
                        await _baseConfirmEmailRepository.DeleteAsync(code.Id);
                    }
                }

                ConfirmEmail confirmEmail = new ConfirmEmail
                {
                    ConfirmCode = GenerateCodeActive(),
                    ExpiryTime = DateTime.Now.AddMinutes(5),
                    IsConfirmed = false,
                    UserId = user.Id,
                };
                await _baseConfirmEmailRepository.CreateAsync(confirmEmail);


                var theEmail = new EmailMessage(new List<string> { user.Email },
                                                        "Receive your confirmation code to reset a new password",
                                                        $"Your confirmation code: {confirmEmail.ConfirmCode}");
                _emailService.SendEmail(theEmail);

                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "The confirmation code has be sent to your email. Please check your email. Thanks",
                    Data = null
                };

            } catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<string>> CreateNewPassword(DataRequestUser_CreateNewPassword request)
        {
            try
            {
                var confirmEmail = await _baseConfirmEmailRepository.GetAsync(x => x.ConfirmCode.Equals(request.ConfirmCode));

                if (confirmEmail == null) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Your code is wrong. Please try again.",
                    Data = null
                };

                if(confirmEmail.ExpiryTime < DateTime.Now) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Your code has been expired.",
                    Data = null
                };

                if (!request.NewPassword.Equals(request.ConfirmPassword))
                {
                    return new ResponseObject<string>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "The passwords does not match",
                        Data = null
                    };
                }

                var user = await _baseUserRepository.GetAsync(x => x.Id == confirmEmail.UserId);
                user.Password = BCryptNet.HashPassword(request.NewPassword);
                user.UpdateTime = DateTime.Now;
                await _baseUserRepository.UpdateAsync(user);

                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "New password created successfully",
                    Data = null
                };
            } catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<string>> AddRolesToUser(Guid userId, List<string> roles)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<string>
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Message = "Something went wrong. Please log in again.",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin")) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You don't have permisison to do this action.",
                    Data = null
                };

                var user = await _baseUserRepository.GetByIdAsync(userId);
                if(user == null) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "User does not exists.",
                    Data = null
                };

                await _userRepository.AddRolesToUserAsync(user, roles);

                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Permission added successfull.",
                    Data = null
                };
            } catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<string>> DeleteRoles(Guid userId, List<string> roles)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<string>
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Message = "Something went wrong. Please log in again.",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin")) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You don't have permisison to do this action.",
                    Data = null
                };

                var user = await _baseUserRepository.GetByIdAsync(userId);
                if (user == null) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "User does not exists.",
                    Data = null
                };

                await _userRepository.DeleteRolesAsync(user, roles);

                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Permission removed successfull.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public ClaimsPrincipal GetCurrentUser()
        {
            return _contextAccessor.HttpContext.User;
        }



        #region private method
        private string GenerateCodeActive()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            long ticks = DateTime.Now.Ticks;

            Random random = new Random((int)(ticks & 0xFFFFFFF));

            string result = new string(
                new char[20].Select(_ => chars[random.Next(chars.Length)]).ToArray()
            );
            return "Coursery_" + result;
        }

        private string GetLinkAvatarDefault()
        {
            return "https://static.vecteezy.com/system/resources/previews/009/292/244/original/default-avatar-icon-of-social-media-user-vector.jpg";
        }

        private JwtSecurityToken CreateToken(List<Claim> claims)
        {
            var authSigningKet = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            //_ = int.TryParse(_configuration["JWT:TokenValidityInHours"], out int tokenValidityInHours);
            var expirationUTC = DateTime.Now.AddHours(1);

            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: expirationUTC,
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSigningKet, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new Byte[64];
            var range = RandomNumberGenerator.Create();
            range.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GetJwtTokenAsync(User user)
        {
            var permissons = await _basePermissionRepository.GetAllAsync(x => x.UserId == user.Id);
            var roles = await _baseRoleRepository.GetAllAsync();

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("PhoneNumber", user.PhoneNumber),
            };

            foreach (var permission in permissons)
            {
                foreach (var role in roles)
                {
                    if (role.Id == permission.Id)
                    {
                        claims.Add(new Claim("Permission", role.RoleName));
                    }
                }
            }

            var userRoles = await _userRepository.GetRolesOfUserAsync(user);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtToken = CreateToken(claims);


            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
            //return new ResponseObject<DataResponseUser_Login>
            //{
            //    StatusCode = StatusCodes.Status200OK,
            //    Message = "Create token successfully",
            //    Data = new DataResponseUser_Login
            //    {
            //        AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            //        RefreshToken = refreshToken,
            //    }
            //};
        }
        
        #endregion

    }
}
