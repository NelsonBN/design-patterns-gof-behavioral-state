using static System.Console;

// Client
var ticket = new Ticket("Ticket description");
WriteLine(ticket);

ticket.InProgress();
WriteLine(ticket);

try { ticket.InProgress(); }
catch (Exception exception) { WriteLine($"Error: {exception.Message}"); }

ticket.InTesting();
WriteLine(ticket);

ticket.Close();
WriteLine(ticket);

try { ticket.Close(); }
catch (Exception exception) { WriteLine($"Error: {exception.Message}"); }



// Context
public partial class Ticket
{
    public Guid Id { get; private set; }
    public string Description { get; private set; }
    public TicketStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public TimeSpan? TimeSpent { get; private set; }

    public Ticket(string description) {
        Id = Guid.NewGuid();
        Description = description;
        Status = new NewStatus(this);
        CreatedAt = DateTime.UtcNow;
    }

    public void InProgress() => Status.InProgress();
    public void InTesting() => Status.InTesting();
    public void Close() => Status.Close();

    public override string ToString()
        => $"[{Id}][{Status}] {Description} -- [ {CreatedAt:yyyy/MM/dd HH:mm:ss.ffffff} / {UpdatedAt:yyyy/MM/dd HH:mm:ss.ffffff} -> {TimeSpent} ]";
}

public partial class Ticket
{
    // State
    public abstract class TicketStatus {
        private string _status;
        protected Ticket _ticket;

        protected TicketStatus(Ticket ticket, string status, DateTime? updatedAt = null) {
            _ticket = ticket;
            _status = status;
            _ticket.UpdatedAt = updatedAt;
        }

        internal abstract void InProgress();
        internal abstract void InTesting();
        internal abstract void Close();

        public override string ToString() => _status;
        public static implicit operator string(TicketStatus status) => status.ToString();
    }

    // Concrete States
    public class NewStatus : TicketStatus {
        public NewStatus(Ticket ticket) : base(ticket, "NEW") {}

        internal override void InProgress() => _ticket.Status = new InProgressStatus(_ticket);
        internal override void InTesting() => throw new InvalidOperationException("Ticket cannot be in testing while new");
        internal override void Close() => _ticket.Status = new ClosedStatus(_ticket);
    }

    // Concrete States
    public class InProgressStatus : TicketStatus {
        internal InProgressStatus(Ticket ticket) : base(ticket, "IN PROGRESS", DateTime.UtcNow) {}

        internal override void InProgress() => throw new InvalidOperationException("Ticket already in progress");
        internal override void InTesting() => _ticket.Status = new InTestingStatus(_ticket);
        internal override void Close() {
            _ticket.Status = new ClosedStatus(_ticket);
            _ticket.TimeSpent = _ticket.UpdatedAt - _ticket.CreatedAt;
        }
    }

    // Concrete States
    public class InTestingStatus : TicketStatus {
        public InTestingStatus(Ticket ticket) : base(ticket, "IN TESTING", DateTime.UtcNow) {}

        internal override void InProgress() => throw new InvalidOperationException("Ticket cannot be in progress while in testing");
        internal override void InTesting() => throw new InvalidOperationException("Ticket already in testing");
        internal override void Close() {
            _ticket.Status = new ClosedStatus(_ticket);
            _ticket.TimeSpent = _ticket.UpdatedAt - _ticket.CreatedAt;
        }
    }

    // Concrete States
    public class ClosedStatus : TicketStatus {
        public ClosedStatus(Ticket ticket) : base(ticket, "CLOSED", DateTime.UtcNow) {}

        internal override void InProgress() => throw new InvalidOperationException("Ticket cannot be in progress while closed");
        internal override void InTesting() => throw new InvalidOperationException("Ticket cannot be in testing while closed");
        internal override void Close() => throw new InvalidOperationException("Ticket already closed");
    }
}
