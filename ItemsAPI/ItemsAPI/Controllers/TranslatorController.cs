using Microsoft.AspNetCore.Mvc;

using ItemsAPI.Models;
using ItemsAPI.Services;


namespace ItemsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslatorController : ControllerBase
    {
        private readonly ITranslatorService _translatorService;

        public TranslatorController(ITranslatorService translatorService)
        {
            _translatorService = translatorService;
        }


        [HttpPost]
        public async Task<IActionResult> Translate(
    TranslateRequest request)
        {
            var translatedText =
                await _translatorService.TranslateAsync(
                    request.Text,
                    request.ToLanguage);

            return Ok(new
            {
                Original = request.Text,
                Translation = translatedText,
                Language = request.ToLanguage
            });
        }

    }
}
