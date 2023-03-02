using AutoMapper;
using System.Drawing;
using TicketDal.Settings;
using TicketGenerator.Data;
using TicketLogic.Model;
using Ticket = TicketDal.Entities.Ticket;

namespace TicketGenerator
{
    internal class Program
    {
        private static readonly List<string> Emails = new()
        {
            "steffen@seventy.mx",
            "steffendionys@gmail.com"
        };

        private static void Main()
        {
            using var context = new DataContext();
            var config = new MapperConfiguration(c => c.CreateMap<Ticket, TicketLogic.Model.Ticket>());

            var mapper = new Mapper(config);

            var sender = new EmailSender(AppSettings.I.EmailConfiguration ?? throw new Exception("EmailConfiguration is not set!"));

            var charge = Guid.NewGuid();
            var flyer = Image.FromFile($@".\{AppSettings.I.FlyerImageName}");

            var counter = 0;
            while (counter < Emails.Count)
            {
                var ticket = new Ticket(charge, Emails[counter]);

                context.Tickets.Add(ticket);

                var logicTicket = mapper.Map<TicketLogic.Model.Ticket>(ticket);
                logicTicket.GenerateTicketBitmap(flyer, ".");

                logicTicket.SendViaMail(sender);

                counter++;
            }

            context.SaveChanges();

            Console.WriteLine($"Charge: {charge}, was Emailed successfully");
        }
    }
}