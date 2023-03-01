using Spire.Barcode;
using System.Drawing;
using TicketDal.Data;
using TicketDal.Entities;

namespace TicketGenerator
{
    internal static class TicketExtension
    {
        public static string Print(this Ticket ticket, Image flyer)
        {
            if (!CheckAndCreate(S.I.TempFolder))
                throw new ArgumentException("Temp folder could not be created!");

            var bs = new BarcodeSettings
            {
                Type = BarCodeType.Code39,
                Data = ticket.ToString()
            };

            var bg = new BarCodeGenerator(bs);
            var barcode = bg.GenerateImage();


            //Combine flyer and generated barcode

            var flyerHeight = (int)Math.Round((double)flyer.Height / flyer.Width * barcode.Width, MidpointRounding.AwayFromZero);

            var bitmapImage = new Bitmap(barcode.Width, barcode.Height + flyerHeight);

            using var g = Graphics.FromImage(bitmapImage);

            g.DrawImage(flyer, 0, 0, barcode.Width, flyerHeight);
            g.DrawImage(barcode, 0, flyerHeight);

            //Save ticket to output folder

            var ticketPath = $@"{S.I.TempFolder}\{bs.Data}.png";
            bitmapImage.Save(ticketPath);

            return ticketPath;
        }

        private static readonly List<string> CreatedDirectories = new();
        private static bool CheckAndCreate(string? path)
        {
            if(string.IsNullOrWhiteSpace(path))
                return false;

            if (CreatedDirectories.Contains(path))
                return true;

            if (Directory.Exists(path))
                return true;

            Directory.CreateDirectory(path);
            CreatedDirectories.Add(path);

            return true;
        }
    }
}
