using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HBHB.Data
{
    public static class UserInfo_Methods

    {

        //-------------< Class: ExtensionMethods >-------------

        public static string getUserIdGuid(this ClaimsPrincipal user)

        {

            //------------< getUserId(User) >------------

            //*returns the current UserID

            if (!user.Identity.IsAuthenticated)

                return null;
            ClaimsPrincipal currentUser = user;

            //< output >

            return currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            //</ output >

            //------------</ getUserId(User) >------------
        }
    }
}
