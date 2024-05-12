
namespace jobPortal.services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment enviroment;
        public ImageService(IWebHostEnvironment enviroment)
        {
            this.enviroment = enviroment;
        }
        public async Task<string> uploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty or null.");
            }
            var uploadFoler = Path.Combine(enviroment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFoler))
            {
                Directory.CreateDirectory(uploadFoler);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(uploadFoler, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
               await file.CopyToAsync(stream);
            }
            return Path.Combine("/uploads/", fileName).Replace("\\", "/");
        }
    }
}
