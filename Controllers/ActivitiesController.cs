using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using api_ods_mace_erasmus.data;
using api_ods_mace_erasmus.helper;
using api_ods_mace_erasmus.Identity;
using api_ods_mace_erasmus.Interfaces;
using api_ods_mace_erasmus.models;

namespace api_ods_mace_erasmus.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        #region constructor thingies
        private readonly IActivityRepository _activityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IMapper? _mapper;


        public ActivityController(IActivityRepository activityRepository, IUserRepository userRepository, IConfiguration configuration/*, IMapper mapper*/)
        {
            _activityRepository = activityRepository;
            _userRepository = userRepository;
            _config = configuration;
            /*_mapper = mapper;*/
        }
        #endregion


        #region proposeActivity

        [Authorize]
        [HttpPost]

        public async Task<IActionResult> proposeActivity(ActivityProposal activityProposal)
        {
            try
            {

                var jwtToken = HelperMethods.decodeToken(_config, HttpContext);

                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "UserID").Value);

                if (activityProposal == null || !_userRepository.UserExists(userId))
                {
                    return BadRequest(new LoginBadResponse
                    {
                        title = "Bad Request",
                        status = 400,
                        detail = "The request was invalid"
                    });
                }

                if (!await HelperMethods.isImageLinkValid(activityProposal.image_uris)){
                    return BadRequest(new LoginBadResponse{
                        title = "Bad Request",
                        status = 400,
                        detail = "one of the links you've provided as source for an image is invalid."
                    });
                }

                var newActivity = new Activity
                {
                    id = Guid.NewGuid(),
                    user_id = userId,
                    activity_state = 1,
                    country = activityProposal.country,
                    explanation = activityProposal.explanation,
                    image_uris = activityProposal.image_uris,
                    latitude = activityProposal.latitude,
                    longitude = activityProposal.longitude,
                    ods = activityProposal.ods,
                    type = activityProposal.type
                };

                _activityRepository.AddActivity(newActivity);

                return CreatedAtAction(nameof(getActivityById), new { id = newActivity.id }, newActivity);

            }
            catch (Exception)
            {
                return Problem();
            }
        }

        #endregion

        #region getActivityById

        [Authorize]
        [HttpGet]
        [Route("activity/{id:guid}")]

        public async Task<IActionResult> getActivityById([FromRoute] Guid id)
        {

            try
            {

                var searchActivity = _activityRepository.getActivity(id);

                if (searchActivity == null)
                {
                    return NotFound();
                }

                var jwtToken = HelperMethods.decodeToken(_config, HttpContext);
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "UserID").Value);
                var isAdmin = bool.Parse(jwtToken.Claims.First(x => x.Type == "admin").Value);

                if (!((searchActivity.user_id == userId) || _userRepository.isReallyAdmin(userId, isAdmin)))
                {
                    return Forbid();
                }

                return Ok(searchActivity);

            }
            catch (Exception)
            {
                return Problem();
            }


        }

        #endregion

    }
}