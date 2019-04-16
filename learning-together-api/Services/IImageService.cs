namespace learning_together_api.Services
{
    using Microsoft.AspNetCore.Http;

    public interface IImageService
    {
        string Store(IFormFile file);
    }
}