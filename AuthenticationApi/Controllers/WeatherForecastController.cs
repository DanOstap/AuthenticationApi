using Amazon.Runtime.Internal;
using AuthenticationApi.Models;
using AuthenticationApi.Servicer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Security.Claims;
using System.Xml.Linq;

namespace AuthenticationApi.Controllers
{
    [ApiController]
    public class CRUD
    {
        private readonly MongoDbService _Service;
        private readonly Roles roles;


        public CRUD(MongoDbService service) {
            _Service = service;  
          }

        [HttpPost("/Regist")]
        public  async Task<IActionResult> Regist(DataBaseModel _model) {
            var DataBaseResultAll = _Service.GetAllAsync().Result;
            var User_name = DataBaseResultAll.Find(name => name.UserName == _model.UserName);

            if (User_name != null) {

                return new BadRequestObjectResult(new { message = "User already exists" });

            }
            _model.Role = roles.Role["User"];
            await _Service.CreateUserAsync(_model);

            var UserId = DataBaseResultAll.Find(name => name.UserName == _model.UserName).Id;
            var claims = new List<Claim> {
            
                new Claim(ClaimTypes.Name, _model.UserName)
            
            };
            return new OkObjectResult(new { message = "User registered successfully"});
        }
        [HttpPost("/Login")]

        public string Authentication(DataBaseModel _model)
        {
            var DataBaseGetAllASync = _Service.GetAllAsync();
            string User_Password = _Service.Hash(_model.Password);
            var User_name = DataBaseGetAllASync.Result.Find(name => name.UserName == _model.UserName);

            if (User_name != null) {
                if (User_Password == _model.Password) { return "Login Accept"; }
                return "Incorect Pasword";
            }
            return "Incorect UserName";
        }
        [HttpPost("/Verification")]
        public string VerificationEmail() {


            return "User Verificate email";
        }
    }
}