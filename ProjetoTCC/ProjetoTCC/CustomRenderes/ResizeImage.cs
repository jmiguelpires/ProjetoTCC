using Android.Graphics;
using ProjetoTCC.CustomRenderes;
using ProjetoTCC.Interface;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(ResizeImage))] //registra a dependencia

namespace ProjetoTCC.CustomRenderes
{
    public class ResizeImage : IResizeImage
    {
        public byte[] Resize(byte[] imageData, int width, int height, bool inCamera)
        {
            // Load the bitmap 
            BitmapFactory.Options options = new BitmapFactory.Options();// Create object of bitmapfactory's option method for further option use
            options.InPurgeable = true; // inPurgeable is used to free up memory while required
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length, options);

            var originalHeight = originalImage.Height;
            var originalWidth = originalImage.Width;

            float newHeight;
            float newWidth;

            if (originalHeight > originalWidth)
            {
                newHeight = height;
                float ratio = originalHeight / height;
                newWidth = originalWidth / ratio;
            }
            else
            {
                newWidth = width;
                float ratio = originalWidth / width;
                newHeight = originalHeight / ratio;
            }

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)newWidth, (int)newHeight, true);

            originalImage.Recycle();

            Matrix matrix = new Matrix();

            if (inCamera)
            {
                matrix.PostRotate(-90);
            }
            var imagem = Bitmap.CreateBitmap(resizedImage, 0, 0, resizedImage.Width, resizedImage.Height, matrix, true);
            resizedImage.Recycle();

            using (MemoryStream ms = new MemoryStream())
            {
                imagem.Compress(Bitmap.CompressFormat.Png, 100, ms);

                imagem.Recycle();

                return ms.ToArray();
            }

        }
    }
}