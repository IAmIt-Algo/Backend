using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Models;
using Service.LevelService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace Backend.Controllers
{
    [System.Web.Http.Authorize]
    public class LevelController : ApiController
    {
        public ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private readonly ILevelService _service;

        public LevelController(ILevelService service)
        {
            _service = service;
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("addAttemp"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> AddAttemp(AddAttempModel model)
        {
            try {
                return Ok(await _service.AddAttempAsync(model));
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }
    }
}
