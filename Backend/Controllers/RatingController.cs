using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Models;
using Service.RatingService;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace Backend.Controllers
{
    [System.Web.Http.Authorize]
    public class RatingController : ApiController
    {
        public ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private readonly IRatingService _service;

        public RatingController(IRatingService service)
        {
            _service = service;
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("increaseRating"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> IncreaseRating(IncreaseRatingModel model)
        {
            model.UserName = User.Identity.GetUserName();
            try
            {
                await _service.IncreaseRatingAsync(model);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
