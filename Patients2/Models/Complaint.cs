namespace Patients2.Models;

public partial class Complaint
{
    public int Id { get; set; }

    public string? Complaint1 { get; set; }

    public virtual ICollection<BasicInputPatient> BasicInputPatients { get; set; } = new List<BasicInputPatient>();
}
