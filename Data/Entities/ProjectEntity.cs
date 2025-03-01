using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity
{
    
    [Key] // Primärnyckel.
    public int Id { get; set; }

    [Required]
    public int ProjectNumber { get; set; }

    [Required] // Gör fältet obligatoriskt.
    public string ProjectName { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; } = DateTime.Now;

    [Required]
    public DateTime EndDate { get; set; } = DateTime.Now.AddDays(30);

    [Required]
    public string ProjectManager { get; set; } = null!;

    [Required]
    public string Service { get; set; } = null!;

    public int? TotalPrice { get; set; }

    [Required]
    public string ProjectStatus { get; set; } = null!;


    [ForeignKey("CustomerId")]
    public int CustomerId { get; set; }
    public CustomerEntity CustomerInfo { get; set; } = null!;

}

