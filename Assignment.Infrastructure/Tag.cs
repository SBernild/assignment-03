namespace Assignment.Infrastructure;
using System.ComponentModel.DataAnnotations;

public class Tag
{
    public int Id { get; set; }
    [StringLength(50), Required]
    public string Name { get; set; }
    public ICollection<WorkItem>? WorkItems { get; set; }

    /* public Tag(int Id, string Name, ICollection<Task> Tasks){
        this.Id = Id;
        this.Name = Name;
        this.Tasks = Tasks;
    } */
}
