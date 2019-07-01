using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;

namespace QuickConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("Not Enough Arguments");
                System.Console.WriteLine("Usage: QuickConvert <sourceimage/directory> <OutputType(png,jpg,gif,bmp,tif,ico)> <OutputDirectory> \n[Optional: -s Target Size (length,width)] No Space Between Comma \n[Optional -i Interpolation Mode] : None - 0, Bicubic - 7, Bilinear - 6, Nearest Neighbor - 5 \n[Optional: -q Quality] 0-100"); Environment.Exit(1);
            }
            InterpolationMode interp = InterpolationMode.HighQualityBicubic;
            if (args.Length == 1)
            {
                int[] sizee = { 0, 0 };
                string appname = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                Console.WriteLine(appname.Split('\\').Last());
                string[] f = appname.Split('\\').Last().Split(',', '.');
                for (int i = 0; i <= 1; i++)
                {
                    sizee[i] = int.Parse(f[i]);
                }
                interp = InterpolationMode.HighQualityBicubic;
                DirectoryInfo d = new DirectoryInfo(args[0]);
                processImage(args[0], System.IO.Directory.GetCurrentDirectory() + "\\" + d.Name.Substring(0, d.Name.Length - d.Extension.Length) + "_"+sizee[0]+"x"+sizee[1] + "."+d.Name.Split('.').Last(), ParseImageFormat(args[0].Split('.').Last()), 100, sizee, InterpolationMode.HighQualityBicubic);;
            }
            if (args[0].ToLower() == "help" || args[0].ToLower() == "-h")
            {
                System.Console.WriteLine("Quickly Converts an image or directory of images to a certain type, quality, and/or size");
                System.Console.WriteLine("Usage: QuickConvert <sourceimage/directory> <OutputType(png,jpg,gif,bmp,tif,ico)> <OutputDirectory> \n[Optional: -s Target Size (length,width)] No Space Between Comma \n[Optional -i Interpolation Mode] : None - 0, Bicubic - 7, Bilinear - 6, Nearest Neighbor - 5 \n[Optional: -q Quality] 0-100"); Environment.Exit(1);
            }

            if (args.Length < 3)
            {
                System.Console.WriteLine("Not Enough Arguments");
                System.Console.WriteLine("Usage: QuickConvert <sourceimage/directory> <OutputType(png,jpg,gif,bmp,tif,ico)> <OutputDirectory> \n[Optional: -s Target Size (length,width)] No Space Between Comma \n[Optional -i Interpolation Mode] : None - 0, Bicubic - 7, Bilinear - 6, Nearest Neighbor - 5 \n[Optional: -q Quality] 0-100"); Environment.Exit(1);
            }


            bool test1 = FileOrDirectoryExists(args[0]);
            if (test1 == false)
            {
                System.Console.WriteLine("Error parsing image path(s)");
                System.Console.WriteLine("Usage: QuickConvert <sourceimage/directory> <OutputType(png,jpg,gif,bmp,tif,ico)> <OutputDirectory> \n[Optional: -s Target Size (length,width)] No Space Between Comma \n[Optional -i Interpolation Mode] : None - 0, Bicubic - 7, Bilinear - 6, Nearest Neighbor - 5 \n[Optional: -q Quality] 0-100"); Environment.Exit(1);
            }

            String imageType = args[1];
            ImageFormat format = ParseImageFormat(imageType);
            Console.WriteLine(format.ToString());

            bool test2 = FileOrDirectoryExists(args[2]);

            if (test2 == false)
            {
                System.Console.WriteLine("Error parsing image path(s)");
                System.Console.WriteLine("Usage: QuickConvert <sourceimage/directory> <OutputType(png,jpg,gif,bmp,tif,ico)> <OutputDirectory> \n[Optional: -s Target Size (length,width)] No Space Between Comma \n[Optional -i Interpolation Mode] : None - 0, Bicubic - 7, Bilinear - 6, Nearest Neighbor - 5 \n[Optional: -q Quality] 0-100");
                Environment.Exit(1);

            }
            int quality = 90;
            int[] size = { 0, 0 };

            if (args.Length > 3)
                if (args[3] != null && (args[3] == "-s"))
                {
                    string[] f = args[4].Split(',');
                    for (int i = 0; i < 2; i++)
                    {
                        size[i] = int.Parse(f[i]);
                    }
                }
            if (args.Length > 3)
                if (args[3] != null && (args[3] == "-q"))
                {
                    quality = int.Parse(args[4]);
                }
            if (args.Length > 3)
                if (args[3] != null && (args[3] == "-i"))
                {
                    interp = (InterpolationMode)int.Parse(args[4]);
                }

            if (args.Length > 5)
                if (args[5] != null && (args[5] == "-i"))
                {
                    interp = (InterpolationMode)int.Parse(args[6]);
                }
            if (args.Length > 5)
                if (args[5] != null && (args[5] == "-q"))
                {
                    quality = int.Parse(args[4]);
                }
            if (args.Length > 5)
                if (args[5] != null && (args[5] == "-s"))
                {
                    string[] f = args[6].Split(',');
                    for (int i = 0; i < 2; i++)
                    {
                        size[i] = int.Parse(f[i]);
                    }
                }

            if (args.Length > 7)
                if (args[7] != null && (args[7] == "-i"))
                {
                    interp = (InterpolationMode)int.Parse(args[8]);
                }
            if (args.Length > 7)
                if (args[7] != null && (args[7] == "-q"))
                {
                    quality = int.Parse(args[8]);
                }
            if (args.Length > 7)
                if (args[7] != null && (args[7] == "-s"))
                {
                    string[] f = args[8].Split(',');
                    for (int i = 0; i < 2; i++)
                    {
                        size[i] = int.Parse(f[i]);
                    }
                }


            FileAttributes attr = File.GetAttributes((args[0]));
            if (attr.HasFlag(FileAttributes.Directory))
            {
                DirectoryInfo d = new DirectoryInfo(args[0]);
                foreach (FileInfo a in d.GetFiles())
                {
                    Console.WriteLine("Processing " + a.Name + " to format: " + imageType + "...");
                    processImage(a.FullName, args[2] + "\\" + a.Name.Substring(0, a.Name.Length - a.Extension.Length) + "." + imageType, format, quality, size, interp);

                }

            }
            else
            {
                FileInfo a = new FileInfo(args[0]);
                Console.WriteLine("Processing " + a.Name + " to format: " + imageType + "...");
                processImage(a.FullName, args[2] + "\\" + a.Name.Substring(0, a.Name.Length - a.Extension.Length) + "." + imageType, format, quality, size, interp);
            }
            Console.WriteLine("Done!");
            Environment.Exit(0);
        }


        public static void processImage(string inputPath, string outputPath, ImageFormat format, int quality, int[] size, InterpolationMode interp)
        {
            Bitmap image = new Bitmap(inputPath);
            Bitmap newImage;

            if (size[0] != 0)
            {
                newImage = ResizeImage(image, size[0], size[1], interp);
                SavePNG(outputPath, newImage, quality, format);
            }
            else
            {
                SavePNG(outputPath, image, quality, format);
            }
        }



        internal static bool FileOrDirectoryExists(string name)
        {
            return (Directory.Exists(name) || File.Exists(name));
        }
        public static ImageFormat ParseImageFormat(string str)
        {
            if (str == "jpg")
                return (ImageFormat)typeof(ImageFormat)
                   .GetProperty("Jpeg", BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase)
                   .GetValue("Jpeg", null);
            if (str == "tif")
                return (ImageFormat)typeof(ImageFormat)
                   .GetProperty("Tiff", BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase)
                   .GetValue("Tiff", null);
            if (str == "ico")
                return (ImageFormat)typeof(ImageFormat)
                   .GetProperty("Icon", BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase)
                   .GetValue("Icon", null);
            return (ImageFormat)typeof(ImageFormat)
                    .GetProperty(str, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase)
                    .GetValue(str, null);
        }

        public static void SavePNG(string path, Image img, int quality, ImageFormat format)
        {

            EncoderParameter qualityParam
            = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            ImageCodecInfo codec = GetEncoderInfo(format);

            EncoderParameters encoderParams
            = new EncoderParameters(1);

            encoderParams.Param[0] = qualityParam;

            MemoryStream mss = new MemoryStream();

            FileStream fs
            = new FileStream(path, FileMode.OpenOrCreate
            , FileAccess.ReadWrite);

            img.Save(mss, codec, encoderParams);
            byte[] matriz = mss.ToArray();

            fs.Write(matriz, 0, matriz.Length);

            mss.Close();
            fs.Close();
        }

        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <param name="interpolate">The interpolation mode to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height, InterpolationMode interpolate)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = interpolate;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
