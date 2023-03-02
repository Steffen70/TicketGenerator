using TicketLogic.Model;

namespace TicketLogic.Common
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
