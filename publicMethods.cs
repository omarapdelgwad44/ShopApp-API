using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace ShopApp_API
{
   static public class publicMethods
    {
        public static string uploadimage(string imagePath)
        {
            var account = new Account("dllzu6e96", "476234853929585", "Xs3zYzhDeX4uobxGgksaQl9falk");
            var cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imagePath)
            };

            var uploadResult = cloudinary.Upload(uploadParams);
            return uploadResult.Url.ToString();
        }

    }
}
