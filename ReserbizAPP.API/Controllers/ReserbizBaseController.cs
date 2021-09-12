using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;

namespace ReserbizAPP.API.Controllers
{
    public class ReserbizBaseController : ControllerBase
    {
        public int CurrentUserId
        {
            get
            {
                return Convert.ToInt32(User.Identity.GetUserClaim(ClaimTypes.NameIdentifier));
            }
        }

        public UserTypeEnum CurrentUserType
        {
            get
            {
                return ((UserTypeEnum)Convert.ToInt32(User.Identity.GetUserClaim(ReserbizClaimTypes.UserType)));
            }
        }
    }
}