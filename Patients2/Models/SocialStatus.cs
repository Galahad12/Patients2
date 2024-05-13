namespace Patients2.Models;

public partial class SocialStatus
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
