[assembly: Xamarin.Forms.Dependency(typeof(FilmInverter.Droid.Renderers.DroidGalleryPath))]
namespace FilmInverter.Droid.Renderers
{
    using Android.OS;
    using PlatformFunctionality;

    public class DroidGalleryPath : IGalleryPath
    {
        public string GetGalleryBasePath()
        {
            return Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures).AbsolutePath;
        }

    }
}