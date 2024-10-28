namespace Domain.Entities;

public class ClosedDay : BaseEntity
{
    public int OwnerId { get; set; }
    public User Owner { get; set; } // Ссылается на владельца

    public DateTime Date { get; set; } 
    public string Reason { get; set; } // Причина закрытия
}