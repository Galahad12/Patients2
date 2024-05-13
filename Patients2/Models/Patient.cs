namespace Patients2.Models;

public partial class Patient
{
    public int Id { get; set; }

    public string? MedicalHistory { get; set; }

    public int? Fullname { get; set; }

    public int? Age { get; set; }

    public int? Sex { get; set; }

    public int? SocialStatus { get; set; }

    public int? Location { get; set; }

    public virtual ICollection<BasicInputCalculation> BasicInputCalculations { get; set; } = new List<BasicInputCalculation>();
    public virtual ICollection<BasicInputPatient> BasicInputPatients { get; set; } = new List<BasicInputPatient>();

    public virtual Location? LocationNavigation { get; set; }

    public virtual Sex? SexNavigation { get; set; }

    public virtual SocialStatus? SocialStatusNavigation { get; set; }
}
