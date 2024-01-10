using HIMS.Data.Master;
using HIMS.Data.Opd;
using HIMS.Model.Master;
using HIMS.Model.Opd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace HIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly I_LoginManager _loginManager;
        private readonly IConfiguration configuration;
        private readonly I_MenuMaster i_MenuMaster;

        public LoginController(I_LoginManager loginManager, IConfiguration configuration, I_MenuMaster i_MenuMaster)
        {
            _loginManager = loginManager;
            this.configuration = configuration;
            this.i_MenuMaster = i_MenuMaster;
        }

        [HttpGet]
        public ActionResult Get(string userName)
        {
            var user = _loginManager.Get(userName);
            return Ok(user);
        }
        [HttpGet]
        [Route("get-menus")]
        public ActionResult GetMenus()
        {
            List<MenuMaster> lstMenu = i_MenuMaster.GetMenus();
            List<MenuModel> finalList = new List<MenuModel>();
            var distinct = lstMenu.Where(x => x.UpId == 0);
            foreach (var ItemData in distinct)
            {
                MenuModel obj = new MenuModel()
                {
                    id = ItemData.Id.ToString(),
                    icon = ItemData.Icon,
                    title = ItemData.LinkName,
                    translate = "",
                    type = "group",
                    children = new List<MenuModel>()
                };
                var levelData = lstMenu.Where(x => x.UpId == Convert.ToInt32(obj.id));
                foreach (var lData in levelData)
                {
                    MenuModel test = new MenuModel()
                    {
                        id = lData.Id.ToString(),
                        icon = lData.Icon,
                        title = lData.LinkName,
                        translate = "",
                        type = "group",
                        children = new List<MenuModel>()
                    };
                    test.children = AddChildtems(lstMenu, test);
                    obj.children.Add(test);
                }
                finalList.Add(obj);
            }

            return Ok(finalList);
        }
        [NonAction]
        private List<MenuModel> AddChildtems(List<MenuMaster> Data, MenuModel obj)
        {
            //var lstData = Data.Where(x => x.KeyNo.StartsWith(obj.key + "_")).ToList();
            var lstData = Data.Where(x => x.UpId == Convert.ToInt32(obj.id)).ToList();
            List<MenuModel> lstChilds = new List<MenuModel>();
            foreach (var objItem in lstData)
            {
                MenuModel objData = new MenuModel()
                {
                    id = objItem.Id.ToString(),
                    icon = objItem.Icon,
                    title = objItem.LinkName,
                    translate = "",
                    type = "group",
                    children = new List<MenuModel>()
                };
                objData.children = AddChildtems(Data, objData);
                lstChilds.Add(objData);
            }
            return lstChilds;
        }


        [HttpPost("token")]
        public ActionResult Token(Login userDetails)
        {
            var user = _loginManager.Get(userDetails.UserName);

            // return null if user not found
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var IsAuthenticated = (userDetails.Password == user.Password && userDetails.UserName == user.UserName);

            if (!IsAuthenticated) return BadRequest(new { message = "Username or password is incorrect" });

            var secret = configuration.GetValue<string>("AppSettings:SECRET");
            //var secret = Environment.GetEnvironmentVariable("SECRET");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)); //Secret
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var role = string.IsNullOrWhiteSpace(user.Role) ? string.Empty : user.Role;
            var authClaims = new[]
               {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, role)
                };
            //UserId, IsAdmin, UserName, UserEmail, UserType should be token
            //Invalid Username or Password
            var token = new JwtSecurityToken(
                null,
                null,
                authClaims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds);

            return Ok(new
            {
                user = new
                {
                    id = user.UserId,
                    userName = user.UserName,
                    MailId = user.MailId,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    role = user.Role,
                    RoleId = user.RoleId,
                    StoreId = user.StoreId,
                    DoctorID = user.DoctorID,
                    MailDomain = user.MailDomain,
                    LoginStatus = user.LoginStatus,
                    IsActive = user.IsActive,
                    IsDoctorType = user.IsDoctorType,
                    IsPOVerify = user.IsPOVerify,
                    IsGRNVerify = user.IsGRNVerify,
                    IsCollection = user.IsCollection,
                    IsBedStatus = user.IsBedStatus,
                    IsCurrentStk = user.IsCurrentStk,
                    IsPatientInfo = user.IsPatientInfo,
                    IsDateInterval = user.IsDateInterval,
                    IsDateIntervalDays = user.IsDateIntervalDays,
                    AddChargeIsDelete = user.AddChargeIsDelete,
                    IsIndentVerify = user.IsIndentVerify,
                    IsPOInchargeVerify = user.IsPOInchargeVerify,
                    IsRefDocEditOpt = user.IsRefDocEditOpt,
                    IsInchIndVfy = user.IsInchIndVfy,
                    IsPharBalClearnace = user.IsPharBalClearnace
                },
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = token.ValidTo
            });
        }
    }
}
