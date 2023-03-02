namespace TicketDal.Entities
{
    public class Ticket
    {
        public Guid TicketId { get; set; } = Guid.NewGuid();
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Redeemed { get; set; } = null;
        public Guid Charge { get; set; }
        public string Email { get; set; }

        public Ticket()
        {

        }

        public Ticket(Guid charge, string email)
        {
            Charge = charge;
            Email = email;
        }

        public override string ToString()
        {
            return TicketId.ToString();
        }
    }
}