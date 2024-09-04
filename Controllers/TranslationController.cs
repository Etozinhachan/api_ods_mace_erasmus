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
    public class TranslationController : ControllerBase
    {
        #region constructor thingies
        private readonly ITranslationRepository _translationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;


        public TranslationController(ITranslationRepository translationRepository, IUserRepository userRepository, IConfiguration configuration)
        {
            _translationRepository = translationRepository;
            _userRepository = userRepository;
            _config = configuration;
        }
        #endregion


        #region proposeTranslation

        [Authorize]
        [HttpPost]

        public async Task<IActionResult> proposeTranslation(TranslationProposal translationProposal)
        {
            try
            {

                var jwtToken = HelperMethods.decodeToken(_config, HttpContext);

                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "UserID").Value);

                if (translationProposal == null || !_userRepository.UserExists(userId))
                {
                    return BadRequest(new LoginBadResponse
                    {
                        title = "Bad Request",
                        status = 400,
                        detail = "The request was invalid"
                    });
                }

                if (!HelperMethods.isJsonValid(translationProposal.translation_json))
                {
                    return BadRequest(new LoginBadResponse
                    {
                        title = "Bad Request",
                        status = 400,
                        detail = "The json you've provided is invalid."
                    });
                }

                var newTranslation = new Translation
                {
                    id = Guid.NewGuid(),
                    user_id = userId,
                    translation_state = 1,
                    language_code = translationProposal.language_code,
                    translation_json = translationProposal.translation_json
                };

                _translationRepository.AddTranslation(newTranslation);

                return CreatedAtAction(nameof(getTranslationById), new { id = newTranslation.id }, newTranslation);

            }
            catch (Exception)
            {
                return Problem();
            }
        }

        #endregion

        #region getAllTranslations

        [HttpGet]
        [Route("translation")]
        public async Task<IActionResult> getAllTranslations()
        {

            try
            {
                return Ok(_translationRepository.GetAllTranslations());

            }
            catch (Exception)
            {
                return Problem();
            }


        }

        #endregion

        #region getTranslationById

        [Authorize]
        [HttpGet]
        [Route("translation/{id:guid}")]
        public async Task<IActionResult> getTranslationById([FromRoute] Guid id)
        {

            try
            {

                var searchTranslation = _translationRepository.GetTranslation(id);

                if (searchTranslation == null)
                {
                    return NotFound();
                }

                return Ok(searchTranslation);

            }
            catch (Exception)
            {
                return Problem();
            }


        }

        #endregion

    }
}