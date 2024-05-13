namespace Patients2.Models;

public partial class BasicInputPatient
{
    public int Id { get; set; }

    public int? Patient { get; set; }

    public int? Height { get; set; }

    public string? Weight { get; set; }

    public int? Sad { get; set; }

    public int? Dad { get; set; }

    public int? Pd { get; set; }

    public int? Css { get; set; }

    public int? Complaints { get; set; }

    public int? Athero { get; set; }

    public int? AffectedArtery { get; set; }

    public int? MainDiagnosis { get; set; }

    public virtual Artery? AffectedArteryNavigation { get; set; }

    public virtual Complaint? ComplaintsNavigation { get; set; }

    public virtual Diagnosis? MainDiagnosisNavigation { get; set; }

    public virtual Patient? PatientNavigation { get; set; }

    public virtual Availability? AvailabilityNavigation { get; set; }
}
