using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CustomerEntity
{
    [Key]
    public int Id { get; set; }

    // Namn måste anges och får max vara 100 tecken.
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
}
