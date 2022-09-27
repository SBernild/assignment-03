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

    public User (int Id, string Name, string Email, IList<WorkItem> WorkItems){

        this.Id = Id;
        this.Name = Name;
        this.Email = Email;
        this.WorkItems = WorkItems;
    }
}
