namespace Patients2.Models;

public partial class BasicInputCalculation
{
    public int Id { get; set; }

    public int? Patient { get; set; }

    public string? Imt { get; set; }

    public int? ImtMean { get; set; }

    public string? Avad { get; set; }

    public virtual Patient? PatientNavigation { get; set; }
    public virtual ImtMean? ImtMeanNavigation { get; set; }
}
