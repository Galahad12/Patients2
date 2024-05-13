namespace Patients2.Models;

public partial class Availability
{
    public int Id { get; set; }

    public string? Availability1 { get; set; }

    public virtual ICollection<BasicInputPatient> BasicInputPatients { get; set; } = new List<BasicInputPatient>();
}
