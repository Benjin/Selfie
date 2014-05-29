using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Capture;
using System.Threading.Tasks;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Phone.UI.Input;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Selfie_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            HardwareButtons.CameraHalfPressed += CameraHalfPressed;
            HardwareButtons.CameraPressed += CameraFullPressed;

            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {


        }

        private int count = 0;

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            count++;
            CounterBlock.Text = "Count: " + count;
        }

        private void LaunchCamera_Click(object sender, RoutedEventArgs e)
        {
            if (camera == null)
            {
                initCamera();
                LaunchCamera.Content = "Take Selfie";
            }
            else
                capturePhoto();
        }

        MediaCapture camera = null;

        private async void initCamera()
        {
            camera = new MediaCapture();
            await camera.InitializeAsync();

            camera.SetPreviewRotation(VideoRotation.Clockwise270Degrees);
            camera.SetRecordRotation(VideoRotation.Clockwise270Degrees);

            CameraPreview.Source = camera;
            await camera.StartPreviewAsync();
        }

        private async Task capturePhoto()
        {
            ImageEncodingProperties imgEncProp = ImageEncodingProperties.CreateJpeg();
            imgEncProp.Width = 640;
            imgEncProp.Height = 480;

            IStorageFile photoStorageFile = await KnownFolders.PicturesLibrary.CreateFileAsync("photo.jpg", CreationCollisionOption.GenerateUniqueName);
            await camera.CapturePhotoToStorageFileAsync(imgEncProp, photoStorageFile);
        }

        private async void CameraHalfPressed(object sender, CameraEventArgs args)
        {
            await camera.VideoDeviceController.FocusControl.FocusAsync();
        }

        private async void CameraFullPressed(object sender, CameraEventArgs args)
        {
            await capturePhoto();
        }
    }
}
