using HBHB.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HBHB.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {


        [HttpGet]
        [Route("api/getLoggedInUser")]
        public async Task<ActionResult<string>> get_UserIdGuid()
        {
            string sIDUserGuid = User.getUserIdGuid();
            if (sIDUserGuid == "") return "";
            return new JsonResult(new { useridguid = sIDUserGuid });
        }

    }
}
