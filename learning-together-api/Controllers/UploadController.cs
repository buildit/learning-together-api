namespace learning_together_api.Controllers
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Services;

    public class UploadController : LearnTogetherController
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly IImageStorageService imageService;
        private readonly ILogger<UsersController> logger;

        public UploadController(IOptions<AppSettings> appSettings, IImageStorageService imageService, ILogger<UsersController> logger)
        {
            this.appSettings = appSettings;
            this.imageService = imageService;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("image"), DisableRequestSizeLimit]
        public ActionResult UploadFile()
        {
            if (this.Request.Form == null || !this.Request.Form.Files.Any())
            {
                return this.BadRequest("No images submitted.");
            }

            try
            {
                IFormFile file = this.Request.Form.Files[0];
                string url = this.imageService.Store(file);
                return this.Ok(url);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Could not store image.");
                return this.StatusCode((int) HttpStatusCode.InternalServerError, $"Upload Failed: {ex.Message}");
            }
        }
    }
}