namespace jobPortal.services
{
    public interface IImageService
    {
        Task<string> uploadImageAsync(IFormFile file);
    }
}
