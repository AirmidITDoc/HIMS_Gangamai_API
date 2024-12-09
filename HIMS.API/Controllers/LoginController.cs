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
using static System.Net.Mime.MediaTypeNames;

namespace HIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly I_LoginManager _loginManager;
        private readonly IConfiguration configuration;
        private readonly I_MenuMaster i_MenuMaster;
        private readonly I_Hospital i_Hospital;

        public LoginController(I_LoginManager loginManager, IConfiguration configuration, I_MenuMaster i_MenuMaster, I_Hospital i_Hospital)
        {
            _loginManager = loginManager;
            this.configuration = configuration;
            this.i_MenuMaster = i_MenuMaster;
            this.i_Hospital = i_Hospital;
        }
        [HttpGet("test-hospital")]
        public ActionResult Test(int Id)
        {
            return Ok(i_Hospital.GetHospitalStoreById(Id));
        }

        [HttpGet]
        public ActionResult Get(string userName)
        {
            var user = _loginManager.Get(userName);
            return Ok(user);
        }
        [HttpGet]
        [Route("get-menus")]
        public ActionResult GetMenus(int RoleId)
        {
            //return Ok(PrepareMenu(RoleId, true));
            return Ok(i_MenuMaster.GetMenus(RoleId, true));
        }
        [HttpGet]
        [Route("get-permission-menu")]
        public ActionResult GetPermissionMenus(int RoleId)
        {
            //return Ok(PrepareMenu(RoleId, false));
            return Ok(i_MenuMaster.GetMenus(RoleId, false));
        }
        //[NonAction]
        //public List<MenuModel> PrepareMenu(int RoleId, bool isActiveMenuOnly)
        //{
        //    List<MenuMaster> lstMenu = i_MenuMaster.GetMenus(RoleId);
        //    List<MenuModel> finalList = new List<MenuModel>();
        //    var distinct = lstMenu.Where(x => x.UpId == 0);
        //    foreach (var ItemData in distinct)
        //    {
        //        MenuModel obj = new MenuModel()
        //        {
        //            id = ItemData.Id.ToString(),
        //            icon = ItemData.Icon,
        //            title = ItemData.LinkName,
        //            translate = "",
        //            type = "collapsable",
        //            children = new List<MenuModel>(),
        //            IsView = ItemData.IsView,
        //            IsAdd = ItemData.IsAdd,
        //            IsDelete = ItemData.IsDelete,
        //            IsEdit = ItemData.IsEdit
        //        };
        //        var levelData = lstMenu.Where(x => x.UpId == Convert.ToInt32(obj.id));
        //        foreach (var lData in levelData)
        //        {
        //            MenuModel test = new MenuModel()
        //            {
        //                id = lData.Id.ToString(),
        //                icon = lData.Icon,
        //                title = lData.LinkName,
        //                translate = "",
        //                type = "collapsable",
        //                children = new List<MenuModel>(),
        //                IsView = lData.IsView,
        //                IsAdd = lData.IsAdd,
        //                IsDelete = lData.IsDelete,
        //                IsEdit = lData.IsEdit
        //            };
        //            test.children = AddChildtems(lstMenu, test, isActiveMenuOnly);
        //            if (test.children.Count == 0)
        //            {
        //                test.type = "item";
        //                test.url = lData.LinkAction;
        //                test.children = null;
        //            }
        //            if ((test?.children?.Count() ?? 0) > 0 || lData.IsView || !isActiveMenuOnly)
        //            {
        //                if (test.children != null)
        //                {
        //                    if (test.children.Count > 0 && test.children.Count == test.children.Count(x => x.IsAdd))
        //                        test.IsAdd = true;
        //                    if (test.children.Count > 0 && test.children.Count == test.children.Count(x => x.IsEdit))
        //                        test.IsEdit = true;
        //                    if (test.children.Count > 0 && test.children.Count == test.children.Count(x => x.IsDelete))
        //                        test.IsDelete = true;
        //                    if (test.children.Count > 0 && test.children.Count == test.children.Count(x => x.IsView))
        //                        test.IsView = true;
        //                }
        //                obj.children.Add(test);
        //            }
        //        }
        //        if (obj.children.Count == 0)
        //        {
        //            obj.type = "item";
        //            obj.url = ItemData.LinkAction;
        //            obj.children = null;
        //        }
        //        if ((obj?.children?.Count ?? 0) > 0 || ItemData.IsView || !isActiveMenuOnly)
        //        {
        //            if (obj.children != null)
        //            {
        //                if (obj.children.Count > 0 && obj.children.Count == obj.children.Count(x => x.IsAdd))
        //                    obj.IsAdd = true;
        //                if (obj.children.Count > 0 && obj.children.Count == obj.children.Count(x => x.IsEdit))
        //                    obj.IsEdit = true;
        //                if (obj.children.Count > 0 && obj.children.Count == obj.children.Count(x => x.IsDelete))
        //                    obj.IsDelete = true;
        //                if (obj.children.Count > 0 && obj.children.Count == obj.children.Count(x => x.IsView))
        //                    obj.IsView = true;
        //            }
        //            finalList.Add(obj);
        //        }
        //    }
        //    return finalList;
        //}
        //[NonAction]
        //private List<MenuModel> AddChildtems(List<MenuMaster> Data, MenuModel obj, bool isActiveMenuOnly)
        //{
        //    List<MenuModel> lstChilds = new List<MenuModel>();
        //    try
        //    {
        //        //var lstData = Data.Where(x => x.KeyNo.StartsWith(obj.key + "_")).ToList();
        //        var lstData = Data.Where(x => x.UpId == Convert.ToInt32(obj.id)).ToList();
        //        foreach (var objItem in lstData)
        //        {
        //            MenuModel objData = new MenuModel()
        //            {
        //                id = objItem.Id.ToString(),
        //                icon = objItem.Icon,
        //                title = objItem.LinkName,
        //                translate = "",
        //                type = "collapsable",
        //                children = new List<MenuModel>(),
        //                IsView = objItem.IsView,
        //                IsAdd = objItem.IsAdd,
        //                IsDelete = objItem.IsDelete,
        //                IsEdit = objItem.IsEdit
        //            };
        //            objData.children = AddChildtems(Data, objData, isActiveMenuOnly);
        //            if (objData.children.Count == 0)
        //            {
        //                objData.type = "item";
        //                objData.url = objItem.LinkAction;
        //                objData.children = null;
        //            }
        //            if ((objData?.children?.Count ?? 0) > 0 || objItem.IsView || !isActiveMenuOnly)
        //            {
        //                if (objData.children != null)
        //                {
        //                    if (objData.children.Count > 0 && objData.children.Count == objData.children.Count(x => x.IsAdd))
        //                        objData.IsAdd = true;
        //                    if (objData.children.Count > 0 && objData.children.Count == objData.children.Count(x => x.IsEdit))
        //                        objData.IsEdit = true;
        //                    if (objData.children.Count > 0 && objData.children.Count == objData.children.Count(x => x.IsDelete))
        //                        objData.IsDelete = true;
        //                    if (objData.children.Count > 0 && objData.children.Count == objData.children.Count(x => x.IsView))
        //                        objData.IsView = true;
        //                }
        //                lstChilds.Add(objData);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return lstChilds;
        //}


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
                    IsPharBalClearnace = user.IsPharBalClearnace,
                    WebRoleId = user.WebRoleId,
                    PharExtOpt = user.PharExtOpt,
                    PharOPOpt = user.PharOPOpt,
                    PharIPOpt = user.PharIPOpt,
                    IsDiscApply =user.IsDiscApply,
                    DiscApplyPer = user.DiscApplyPer
                },
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = token.ValidTo
            });
        }
    }
}
