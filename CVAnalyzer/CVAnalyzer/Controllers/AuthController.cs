using System.Web.Http.Results;
using CVAnalyzer.Authentication.service;
using CVAnalyzer.Models;
using CVAnalyzer.Repositories;
using CVAnalyzer.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CVAnalyzer.Authentication.Utility;

namespace CVAnalyzer.Controllers
{
    public class AuthController : ApiController
    {
        private UserRepository _userRepository;
        private AuthService _authService;

        public AuthController()
        {
            _userRepository = new UserRepository(new AppContext());
            _authService = new AuthService(new AppContext());
        }



        [Route("account/signin")]
        [HttpPost]
        public IHttpActionResult Signin([FromBody] SigninInfoViewModel signinInfoViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.getByEmail(signinInfoViewModel.Email);
                if (user == null || !user.Password.Equals(signinInfoViewModel.Password))
                {
                    return BadRequest("email or password is incorrect");
                }

                string tokenValue = _authService.CreateAuthToken(user.Id);
                return Ok(new LoggedInUserInfoViewModel
                {
                    UserId = user.Id,
                    TokenValue = tokenValue
                });
            }
            return BadRequest();
        }

        [Route("account/signup")]
        public IHttpActionResult Signup(SignupInfoViewModel signupInfoViewModel)
        {
            if (ModelState.IsValid)
            {
                if (signupInfoViewModel.Password != signupInfoViewModel.ConfirmPassword)
                {
                    return BadRequest("Password not matched");
                }
                if (_userRepository.getByEmail(signupInfoViewModel.Email) != null)
                {
                    return BadRequest("user exists");
                }
                var user = new User
                               {
                                   Email = signupInfoViewModel.Email,
                                   Password = signupInfoViewModel.Password
                               };
                if (_userRepository.Add(user))
                {
                    string tokenValue = _authService.CreateAuthToken(user.Id);
                    return Ok(new LoggedInUserInfoViewModel
                    {
                        UserId = user.Id,
                        TokenValue = tokenValue
                    });
                }
                return BadRequest("user cannot be created");
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("account/logout")]
        [TokenAuthorize]
        public IHttpActionResult Logout([FromBody]int userId)
        {
            _authService.DeleteToken(userId);
            return Ok("successfully logout");
        }
    }
}
