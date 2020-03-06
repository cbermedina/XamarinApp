using System;
using Xamarin.Forms;

namespace SuBienApp.Classes
{
    public class ImageSourceEventArgs : EventArgs
    {
        public ImageSourceEventArgs(ImageSource imageSource)
        {
            if (imageSource == null) throw new ArgumentNullException("imageSource");

            ImageSource = imageSource;
        }

        public ImageSource ImageSource { get; private set; }
    }
}
