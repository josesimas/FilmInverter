namespace FilmInverter
{
    using SkiaSharp;
    using SkiaSharp.Views.Forms;
    using Xamarin.Forms.Xaml;

    //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/effects/image-filters
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagePage
    {
        private readonly string _path;
        private readonly bool _invert;
        private readonly bool _blackAndWhite;
        private SKBitmap _bitmap;
        private SKCanvasView _canvasView;


        public ImagePage()
        {
            InitializeComponent();

            _canvasView = new SKCanvasView();
            _canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Container.Content = _canvasView;
        }

        public ImagePage(string path, bool invert, bool blackAndWhite) : this()
        {
            _path = path;
            _invert = invert;
            _blackAndWhite = blackAndWhite;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _bitmap = Rotate(_path);

        }

        protected override void OnDisappearing()
        {
            _canvasView.PaintSurface -= OnCanvasViewPaintSurface;
            _canvasView = null;
            base.OnDisappearing();
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            if (_bitmap is null)
                return;

            var info = args.Info;
            var surface = args.Surface;
            var canvas = surface.Canvas;
            
            canvas.Clear();

            using (var paint = new SKPaint())
            {
                //B&W
                //paint.ColorFilter = SKColorFilter.CreateColorMatrix(new[]
                //    {
                //        0.21f, 0.72f, 0.07f, 0, 0,
                //        0.21f, 0.72f, 0.07f, 0, 0,
                //        0.21f, 0.72f, 0.07f, 0, 0,
                //        0,     0,     0,     1, 0
                //    });
                //

                //Invert colors

                if (_invert)
                    paint.ColorFilter = SKColorFilter.CreateColorMatrix(new[]
                    {
                        -1.0f,  0.0f,  0.0f,  1.0f,  0.0f,
                        0.0f,  -1.0f,  0.0f,  1.0f,  0.0f,
                        0.0f,  0.0f,  -1.0f,  1.0f,  0.0f,
                        1.0f,  1.0f,  1.0f,  1.0f,  0.0f
                    });

                canvas.DrawBitmap(_bitmap, info.Rect, BitmapStretch.Uniform, paint: paint);
            }
        }

        private SKBitmap Rotate(string path)
        {
            using (var bitmap = SKBitmap.Decode(path))
            {
                var rotated = _blackAndWhite
                    ? new SKBitmap(bitmap.Height, bitmap.Width, SKColorType.Gray8, SKAlphaType.Unknown)
                    : new SKBitmap(bitmap.Height, bitmap.Width);

                using (var surface = new SKCanvas(rotated))
                {
                    surface.Translate(rotated.Width, 0);
                    surface.RotateDegrees(90);
                    surface.DrawBitmap(bitmap, 0, 0);
                }

                return rotated;
            }
        }
        
    }
}