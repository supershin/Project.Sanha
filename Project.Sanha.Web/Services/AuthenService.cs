using System;
using Project.Sanha.Web.Models;
using Project.Sanha.Web.Repositories;

namespace Project.Sanha.Web.Services
{
	public interface IAuthenService
    {
        int Authentication(string email);
        LoginResp VerifyLogin(string userName, string password);
    }
    public class AuthenService : IAuthenService
    {
        private readonly IAuthenRepo _authen;

        public AuthenService(IAuthenRepo authen)
        {
            _authen = authen;
        }

        public int Authentication(string email)
        {
            var authen = _authen.Authentication(email);

            return authen;
        }

        public LoginResp VerifyLogin(string userName, string password)
        {
            LoginResp login = _authen.VerifyLogin(userName, password);

            return login;
        }
    }
}

