namespace Patients2.Models;

public partial class Sex
{
    public int Id { get; set; }

    public string? Sex1 { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
