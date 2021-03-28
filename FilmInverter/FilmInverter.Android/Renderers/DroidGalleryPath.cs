[assembly: Xamarin.Forms.Dependency(typeof(FilmInverter.Droid.Renderers.DroidGalleryPath))]
namespace FilmInverter.Droid.Renderers
{
    using Android;
    using Android.Graphics;
    using Android.Net;
    using Android.OS;
    using Android.Provider;
    using Android.Util;
    using Java.Lang;
    using PlatformFunctionality;

    public class DroidGalleryPath : IGalleryPath
    {
        public string GetGalleryBasePath()
        {
            //return System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDcim);

            //var context = Android.App.Application.Context;
            //return context.GetExternalFilesDir(Android.OS.Environment.ExternalStorageState).AbsolutePath;

            return Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures).AbsolutePath;
        }
    }
}