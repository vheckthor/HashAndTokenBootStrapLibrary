using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;

namespace PasswordHasherandJwtTokenProvider
{
    public class Claime : IClaim
    {
        /// <summary>
        /// To get the unique identifier of the user that is loggedIn and compare it with the token
        /// This can be username or GUID
        /// User is passed in when the [Authorization]  attribute is used on the class, the User is from the "System.Security.Claims.ClaimsPrincipal"
        /// Any controller that implements ControllerBase will have the User properties
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public string GetClaims(ClaimsPrincipal User)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return claim;
        }
    }
    
}
