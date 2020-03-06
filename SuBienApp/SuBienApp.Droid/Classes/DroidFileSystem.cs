using System;
using System.IO;
using System.Net;
using Android.Graphics;
using SuBienApp.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(SuBienApp.Droid.Classes.DroidFileSystem))]

namespace SuBienApp.Droid.Classes
{
    public class DroidFileSystem : IFileSystem
    {
        private string directoryFile;

        public string DirectoryFile
        {
            get
            {
                if (string.IsNullOrEmpty(directoryFile))
                {
                    var dir =
                        Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
                    directoryFile = dir.AbsolutePath + "/Camera";
                    //System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }
                return directoryFile;
            }
        }

        public bool ResizeImageAndSave(string pathFrom, string pathTo, string nameFile, float width, float height)
        {
            try
            {
                var filename = System.IO.Path.Combine(pathTo, nameFile);
                var webClient = new WebClient();
                byte[] imageData = webClient.DownloadData(pathFrom);
                // Load the bitmap
                Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
                var resizedImage = scaleBitmap(originalImage, width, height);
                byte[] resutArray;
                using (MemoryStream ms = new MemoryStream())
                {
                    resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                    resutArray = ms.ToArray();
                }
                File.WriteAllBytes(filename, resutArray);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Bitmap scaleBitmap(Bitmap bm, float maxWidth, float maxHeight)
        {
            int width = bm.Width;
            int height = bm.Height;

            // Log.v("Pictures", "Width and height are " + width + "--" + height);

            if (width > height)
            {
                // landscape
                float ratio = (float) width/maxWidth;
                width = (int) maxWidth;
                height = (int) (height/ratio);
            }
            else if (height > width)
            {
                // portrait
                float ratio = (float) height/maxHeight;
                height = (int) maxHeight;
                width = (int) (width/ratio);
            }
            else
            {
                // square
                height = (int) maxHeight;
                width = (int) maxWidth;
            }

            // Log.v("Pictures", "after scaling Width and height are " + width + "--" + height);

            bm = Bitmap.CreateScaledBitmap(bm, width, height, true);
            return bm;
        }


        public byte[] GetSizeImage(string pathFrom)
        {
            try
            {
                var webClient = new WebClient();
                return webClient.DownloadData(pathFrom);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}