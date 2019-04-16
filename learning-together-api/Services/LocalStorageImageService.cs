using System;
using System.IO;
using learning_together_api;
using learning_together_api.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

public class LocalStorageImageService : IImageService
{
    private readonly IHostingEnvironment environment;
    private readonly AppSettings appSettings;

    public LocalStorageImageService(IHostingEnvironment environment, IOptions<AppSettings> appSettings)
    {
        this.environment = environment;
        this.appSettings = appSettings.Value;
    }

    public string Store(IFormFile file)
    {
        string webRoot = this.environment.WebRootPath;
        string datePath = $"{DateTime.Today.ToShortDateString()}";
        string targetPath = Path.Combine(webRoot, this.appSettings.ImageRootPath, datePath);

        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
        }

        string filename = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";

        targetPath = $"{targetPath}/{filename}";

        if (file.Length > 0)
        {
            using (FileStream stream = new FileStream(targetPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"{this.appSettings.StaticServePath}/{datePath}/{filename}";
        }

        return string.Empty;
    }
}