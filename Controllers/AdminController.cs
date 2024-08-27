
using api_ods_mace_erasmus.data;
using api_ods_mace_erasmus.helper;
using api_ods_mace_erasmus.Identity;
using api_ods_mace_erasmus.Interfaces;
using api_ods_mace_erasmus.models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_ods_mace_erasmus.Controllers
{
    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        #region constructor thingies
        private readonly IActivityRepository _activityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IMapper? _mapper;


        public AdminController(IActivityRepository activityRepository, IUserRepository userRepository, IConfiguration configuration/*, IMapper mapper*/)
        {
            _activityRepository = activityRepository;
            _userRepository = userRepository;
            _config = configuration;
            /*_mapper = mapper;*/
        }
        #endregion


        #region AcceptActivity


        [HttpGet]
        [Route("accept/{activity_id:guid}")]
        public async Task<IActionResult> accept_activity_proposal([FromRoute] Guid activity_id)
        {
            var jwtToken = HelperMethods.decodeToken(_config, HttpContext);

            var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "UserID").Value);

            if (!_userRepository.UserExists(userId))
            {
                return BadRequest(new LoginBadResponse
                {
                    title = "Bad Request",
                    status = 400,
                    detail = "The request is invalid."
                });
            }

            var search_activity = _activityRepository.getActivity(activity_id);

            if (search_activity == null)
            {
                return NotFound();
            }

            search_activity.activity_state = 2;

            _activityRepository.ActivityModified(search_activity);

            return Ok();

        }

        #endregion

        #region RejectActivity


        [HttpGet]
        [Route("reject/{activity_id:guid}")]
        public async Task<IActionResult> reject_activity_proposal([FromRoute] Guid activity_id)
        {
            var jwtToken = HelperMethods.decodeToken(_config, HttpContext);

            var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "UserID").Value);

            if (!_userRepository.UserExists(userId))
            {
                return BadRequest(new LoginBadResponse
                {
                    title = "Bad Request",
                    status = 400,
                    detail = "The request is invalid."
                });
            }

            var search_activity = _activityRepository.getActivity(activity_id);

            if (search_activity == null)
            {
                return NotFound();
            }

            search_activity.activity_state = 0;

            _activityRepository.ActivityModified(search_activity);

            return Ok();

        }

        #endregion

        #region EditActivity

        [HttpPatch]
        [Route("edit/{activity_id:guid}")]
        public async Task<IActionResult> edit_activity([FromRoute] Guid activity_id, [FromBody] AdminActivityProposal adminActivityProposal)
        {
            var jwtToken = HelperMethods.decodeToken(_config, HttpContext);

            var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "UserID").Value);

            if (!_userRepository.UserExists(userId))
            {
                return BadRequest(new LoginBadResponse
                {
                    title = "Bad Request",
                    status = 400,
                    detail = "The request is invalid."
                });
            }

            if (adminActivityProposal == null)
            {
                return BadRequest();
            }

            var search_activity = _activityRepository.getActivity(activity_id);

            if (search_activity == null)
            {
                return NotFound();
            }

            if (adminActivityProposal.image_uris != null)
            {
                if (!await HelperMethods.isImageLinkValid(adminActivityProposal.image_uris))
                {
                    return BadRequest(new LoginBadResponse
                    {
                        title = "Bad Request",
                        status = 400,
                        detail = "one of the links you've provided as source for an image is invalid."
                    });
                }
                search_activity.image_uris = adminActivityProposal.image_uris;
            }

            if (adminActivityProposal.type != null)
            {
                search_activity.type = adminActivityProposal.type;
            }

            if (adminActivityProposal.ods != null)
            {
                search_activity.ods = adminActivityProposal.ods;
            }

            if (adminActivityProposal.country != null)
            {
                search_activity.country = adminActivityProposal.country;
            }

            if (adminActivityProposal.explanation != null)
            {
                search_activity.explanation = adminActivityProposal.explanation;
            }

            if (adminActivityProposal.latitude != null)
            {
                search_activity.latitude = (decimal)adminActivityProposal.latitude;
            }

            if (adminActivityProposal.longitude != null)
            {
                search_activity.longitude = (decimal)adminActivityProposal.longitude;
            }

            if (adminActivityProposal.activity_state != null)
            {
                search_activity.activity_state = (byte)adminActivityProposal.activity_state;
            }

            _activityRepository.ActivityModified(search_activity);

            return Ok();

        }

        #endregion

    }
}