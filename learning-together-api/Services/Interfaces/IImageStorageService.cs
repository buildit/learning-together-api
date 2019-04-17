namespace learning_together_api.Services
{
    using Microsoft.AspNetCore.Http;

    public interface IImageStorageService
    {
        string Store(IFormFile file);
    }
}