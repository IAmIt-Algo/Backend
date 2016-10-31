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
        private readonly IRatingService _service;

        public RatingController(IRatingService service)
        {
            _service = service;
        }
        [System.Web.Http.HttpGet, System.Web.Http.Route("getRating"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetRating()
        {
            return Ok(await _service.GetRatingPositionAsync(User.Identity.GetUserName()));
        }
    }
}
