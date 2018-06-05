using System.Drawing;

namespace KS.Core.FileSystemProvide.Base
{
    public interface IImageManager
    {
        Image CreateThumbnail(Image imgPhoto, int width = 0, int height = 0, int compression = 0);
        Image CreateExteraSmall(Image imgPhoto, int width = 0, int height = 0, int compression = 0);
        Image CreateSmall(Image imgPhoto, int width = 0, int height = 0, int compression = 0);
        Image CreateMedium(Image imgPhoto, int width = 0, int height = 0, int compression = 0);
        Image CreateExteraLarg(Image imgPhoto, int width = 0, int height = 0, int compression = 0);
        Image AddWaterMarkFromImage(Image imgPhoto, Image imgWaterMark, int width = 0, int height = 0, int x = 0, int y = 0);
        Image AddWaterMarkFromText(Image imgPhoto, string waterMarkText, int width, int height, int x = 0, int y = 0);
        Image Resize(Image imgPhoto, int width = 0, int height = 0, int percent = 0);
    }
}
