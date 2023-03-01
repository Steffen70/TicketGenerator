using System.Drawing;
using Ionic.Zip;
using TicketDal.Data;
using TicketDal.Entities;

namespace TicketGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using var context = new DataContext();

            if (!args.Any() || !int.TryParse(args[0], out var n))
                throw new ArgumentException("Enter how many tickets you want to generate! (args[0])");

            var charge = Guid.NewGuid();
            var flyer = Image.FromFile($@".\{S.I.FlyerImageName}");

            var counter = 0;
            while (counter < n)
            {
                var ticket = new Ticket(charge);

                context.Tickets.Add(ticket);

                Console.WriteLine($"Ticket: {ticket.TicketId}, {ticket.Print(flyer)}");

                counter++;
            }

            //Zip tickets

            using (var zip = new ZipFile())
            {
                zip.AddDirectory(S.I.TempFolder, charge.ToString());
                zip.Save($"{charge}.zip");
            }

            //Clear temp

            var dir = new DirectoryInfo(S.I.TempFolder ?? throw new ArgumentException("Configure the temp path!"));
            dir.Delete(true);

            //Save tickets to db

            context.SaveChanges();

            Console.WriteLine($"Charge: {charge}, was packaged successfully");
        }
    }
}