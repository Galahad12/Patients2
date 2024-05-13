namespace Patients2.Models;

public partial class Diagnosis
{
    public int Id { get; set; }

    public string? Diagnosis1 { get; set; }

    public virtual ICollection<BasicInputPatient> BasicInputPatients { get; set; } = new List<BasicInputPatient>();
}
