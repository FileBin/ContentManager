using System.Text;
using ContentManager.Api.Contracts.Domain.Enum;
using ContentManager.Api.Contracts.Domain.Exceptions;

namespace ContentManager.Api.Helpers.Extensions;

public static class FileFormatExtensions {
    public static ContentType GetContentType(this Stream stream) {
        var imageFormat = stream.GetImageFormat();

        if(imageFormat != ImageFormat.Unknown) {
            if(imageFormat == ImageFormat.GIF)
                return ContentType.Gif;

            return ContentType.Picture;
        }

        throw new UnknownFileFormatException();
    }

    public static ImageFormat GetImageFormat(this Stream stream) {
        // see http://www.mikekunz.com/image_file_header.html  
        byte[] bmp = Encoding.ASCII.GetBytes("BM");
        byte[] gif = Encoding.ASCII.GetBytes("GIF");
        byte[] png = [137, 80, 78, 71];
        byte[] tiff = [73, 73, 42];
        byte[] tiff2 = [77, 77, 42];
        byte[] jpeg = [255, 216, 255, 224];
        byte[] jpeg2 = [255, 216, 255, 225];

        byte[] buffer = new byte[4];

        int bytes_read = stream.Read(buffer, 0, buffer.Length);
        stream.Seek(0, SeekOrigin.Begin);

        if (bytes_read < 2)
            return ImageFormat.Unknown;


        if (bmp.SequenceEqual(buffer.Take(bmp.Length)))
            return ImageFormat.BMP;

        if (gif.SequenceEqual(buffer.Take(gif.Length)))
            return ImageFormat.GIF;

        if (png.SequenceEqual(buffer.Take(png.Length)))
            return ImageFormat.PNG;

        if (tiff.SequenceEqual(buffer.Take(tiff.Length)))
            return ImageFormat.TIFF;

        if (tiff2.SequenceEqual(buffer.Take(tiff2.Length)))
            return ImageFormat.TIFF;

        if (jpeg.SequenceEqual(buffer.Take(jpeg.Length)))
            return ImageFormat.JPEG;

        if (jpeg2.SequenceEqual(buffer.Take(jpeg2.Length)))
            return ImageFormat.JPEG;

        return ImageFormat.Unknown;
    }

}