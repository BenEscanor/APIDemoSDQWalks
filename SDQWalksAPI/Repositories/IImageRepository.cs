using SDQWalksAPI.Models.Domain;

namespace SDQWalksAPI.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
