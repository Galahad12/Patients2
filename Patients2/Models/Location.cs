namespace Patients2.Models;

public partial class Location
{
    public int Id { get; set; }

    public string? Location1 { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
