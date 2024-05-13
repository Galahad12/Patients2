namespace Patients2.Models;

public partial class Artery
{
    public int Id { get; set; }

    public string? Artery1 { get; set; }

    public virtual ICollection<BasicInputPatient> BasicInputPatients { get; set; } = new List<BasicInputPatient>();
}
