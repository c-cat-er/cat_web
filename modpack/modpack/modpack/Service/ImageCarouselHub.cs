using Microsoft.AspNetCore.SignalR;

namespace modpack.Service
{
    public class ImageCarouselHub : Hub
    {
        public async Task UpdateImages(List<string> imagePaths, List<string> descriptions)
        {
            try
            {
                Console.WriteLine("hub成功");
                await Clients.All.SendAsync("ReceiveImagePaths", imagePaths, descriptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating images: {ex.Message}");
            }
        }
    }
}
