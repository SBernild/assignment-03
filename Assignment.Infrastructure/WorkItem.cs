namespace Assignment.Infrastructure;
using System.ComponentModel.DataAnnotations;
using Assignment.Core;

public class WorkItem
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100), Required]
    public string Title { get; set; }
    public User AssignedTo { get; set; }
    [MaxLength(int.MaxValue)]
    public string? Description { get; set; }
    [Required]
    public Core.State State { get; set; }
    public virtual IReadOnlyCollection<string>? Tags { get; }


    /*
    public WorkItem(int id, string t, User? a, string? d, State s, ICollection<Tag>? tag)
    {
        this.Id = id;
        this.Title = t;
        this.AssignedTo = a;
        this.Description = d;
        this.State = s;
        this.Tags = tag;
    }
    */

    

}
/*
public enum State {
    New,
    Active,
    Resolved,
    Closed,
    Removed
}
*/
