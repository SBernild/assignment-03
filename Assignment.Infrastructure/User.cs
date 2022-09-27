namespace Assignment.Infrastructure;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    [StringLength(100), Required]
    public string Name { get; set; }
    [StringLength(100), Required]
    public string Email { get; set; }
    public IList<WorkItem> WorkItems { get; set; }
}
