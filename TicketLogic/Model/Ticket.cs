using Spire.Barcode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;

namespace TicketLogic.Model
{
    public class Ticket : TicketDal.Entities.Ticket
    {
        public void GenerateTicketBitmap(Image flyer, string wwwroot)
        {
            var bs = new BarcodeSettings
            {
                Type = BarCodeType.Code39,
                Data = ToString()
            };

            var bg = new BarCodeGenerator(bs);
            var barcode = bg.GenerateImage();


            //Combine flyer and generated barcode

            var flyerHeight = (int)Math.Round((double)flyer.Height / flyer.Width * barcode.Width, MidpointRounding.AwayFromZero);

            var bitmapImage = new Bitmap(barcode.Width, barcode.Height + flyerHeight);

            using var g = Graphics.FromImage(bitmapImage);

            g.DrawImage(flyer, 0, 0, barcode.Width, flyerHeight);
            g.DrawImage(barcode, 0, flyerHeight);

            var ticketPath = $@"{wwwroot}\{bs.Data}.png";
            bitmapImage.Save(ticketPath);
        }

        public void SendViaMail(EmailSender sender)
        {
            sender.SendEmail(new Message(new String[] { this.Email }, $"Millennium Event Ticket {TicketId}", new BodyBuilder() { HtmlBody = @"<p>Hey!</p><img src=""https://www.cycleworld.com/resizer/binQnlJHOY8nORx_oNP4vUy7Zdw=/1440x960/filters:focal(847x513:857x523)/cloudfront-us-east-1.images.arcpublishing.com/octane/XQXAGRBLNVFU3BHLGBE74QZG4A.jpg"">" }.ToMessageBody()));
        }
    }
}
