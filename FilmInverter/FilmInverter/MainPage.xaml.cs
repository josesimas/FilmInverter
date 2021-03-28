namespace FilmInverter
{
    using System;
    using System.IO;
    using PlatformFunctionality;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public partial class MainPage
    {
        private string _path;

        public MainPage()
        {
            InitializeComponent();
        }
        
        private async void Take_OnClicked(object sender, EventArgs e)
        {
            var galleryPathService = DependencyService.Resolve<IGalleryPath>();
            var galleryBasePath = galleryPathService.GetGalleryBasePath();
            
            var photo = await MediaPicker.CapturePhotoAsync();
            if(photo?.FullPath is null)
                return;
            
            _path = Path.Combine(galleryBasePath, Path.GetFileName(photo.FullPath));
            try
            {
                var status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (status == PermissionStatus.Granted)
                    File.Copy(photo.FullPath, _path);
                File.Delete(photo.FullPath);
            }
            catch (UnauthorizedAccessException)
            {
                //Work in readonly mode, this phone does not let you save the files to the gallery.
                _path = photo.FullPath;
            }
        }

        private async void Pick_OnClicked(object sender, EventArgs e)
        {
            var photo = await MediaPicker.PickPhotoAsync();
            if (photo != null)
                _path = photo.FullPath;
        }

        private async void Invert_OnClicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(_path))
                return;
            await Navigation.PushModalAsync(new ImagePage(_path, true, false));
        }

        private async void View_OnClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_path))
                return;
            await Navigation.PushModalAsync(new ImagePage(_path, false, false));
        }

        private async void InvertBw_OnClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_path))
                return;
            await Navigation.PushModalAsync(new ImagePage(_path, true, true));
        }
    }
}
