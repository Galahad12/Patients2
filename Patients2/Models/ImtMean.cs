namespace Patients2.Models;

public partial class ImtMean
{
    public int Id { get; set; }

    public string? Mean { get; set; }

    public virtual ICollection<BasicInputCalculation> BasicInputCalculations { get; set; } = new List<BasicInputCalculation>();
}
