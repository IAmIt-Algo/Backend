using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Models;
using Service.LevelService;
using Service.RatingService;
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

        private readonly ILevelService _levelService;
        private readonly IRatingService _ratingService;

        public LevelController(ILevelService levelService, IRatingService ratingService)
        {
            _levelService = levelService;
            _ratingService = ratingService;
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("addAttempt"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> AddAttempt(AddAttemptModel model)
        {
            model.UserId = User.Identity.GetUserId();
            model.UserName = User.Identity.GetUserName();
            try {
                await _levelService.AddAttemptAsync(model);
                return Ok();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
    }
}
