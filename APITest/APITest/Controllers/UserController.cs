using APITest.Dto;
using APITest.Service.Interface;
using APITest.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

namespace APITest.Controller
{
    [ApiController]
    [Route("user/")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenManager _tokenManager;

        public UserController(IUserService userService, ITokenManager tokenManager)
        {
            _userService = userService;
            _tokenManager = tokenManager;
        }

        [HttpPost("register")]
        public async Task<ResultDto> register([FromBody] RegistrationDto dto)
        {
            RequestHandlerAsync<UserDto> requestHandler = new RequestHandlerAsync<UserDto>();
            return await requestHandler.getResultAsync(() => _userService.register(dto));
        }

        [HttpPost("login")]
        public async Task<ResultDto> login([FromBody] LoginDto dto)
        {
            RequestHandlerAsync<UserDto> requestHandler = new RequestHandlerAsync<UserDto>();
            return await requestHandler.getResultAsync(() => _userService.login(dto));
        }
        
        [HttpPost("logout")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ResultDto> logout()
        {
            await _tokenManager.DeactivateCurrentToken();
            ResultDto resultDto = new ResultDto();
            resultDto.isSuccess = true;
            resultDto.error = null;
            resultDto.data = null;
            return resultDto;
        }

        [HttpPost("delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ResultDto> delete()
        {
            var token = HttpContext.Request.Headers[HeaderNames.Authorization];
            RequestHandlerAsync<UserDto> requestHandler = new RequestHandlerAsync<UserDto>();
            return await requestHandler.getResultAsync(() => _userService.deleteUser(token));
        }

        [HttpPost("list")]
        public async Task<ResultDto> getSimilarUsers([FromBody] string email)
        {
            RequestHandlerAsync<List<UserDto>> requestHandler = new RequestHandlerAsync<List<UserDto>>();
            return await requestHandler.getResultAsync(() => _userService.getSimilarUsers(email));
        }

        [HttpPost("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ResultDto> updateUser([FromBody] UpdateUserDto dto)
        {
            var token = HttpContext.Request.Headers[HeaderNames.Authorization];

            RequestHandlerAsync<UserDto> requestHandler = new RequestHandlerAsync<UserDto>();

            return await requestHandler.getResultAsync(() => _userService.updateUser(token, dto));
        }
    }
}