[assembly: Xamarin.Forms.Dependency(typeof(FilmInverter.iOS.Renderers.IosGalleryPath))]
namespace FilmInverter.iOS.Renderers
{
    using PlatformFunctionality;

    public class IosGalleryPath : IGalleryPath
    {
        public string GetGalleryBasePath()
        {
            return string.Empty;
        }
    }
}