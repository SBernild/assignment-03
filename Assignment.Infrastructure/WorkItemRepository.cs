namespace Assignment.Infrastructure;
using System.Collections.Generic;
using Assignment.Core;

public class WorkItemRepository : IWorkItemRepository
{
    private readonly KabanContext _context;

    public WorkItemRepository(KabanContext context){
        _context = context;
    }
    (Response Response, int WorkItemId) Create(WorkItemCreateDTO workItem)
    {
        var entity = new WorkItem{
            Title = workItem.Title
        };

        var exists = from w in _context.WorkItems
        where w.Title == workItem.Title
        select new WorkItemDTO(w.Id, w.Title, w.AssignedTo.Name, w.Tags, w.State);

        if (exists.Any()){
            return (Response.Conflict, -1);
        }

        _context.WorkItems.Add(entity);
        _context.SaveChanges();

        return (Response.Created, entity.Id);
    }
    WorkItemDetailsDTO Find(int workItemId)
    {
        var workItem = _context.WorkItems.Find(workItemId);

        if(workItem == null){
            return null;
        }

        return new WorkItemDetailsDTO(workItem.Id, workItem.Title, workItem.Description, workItem.Created, workItem.AssignedToName, workItem.Tags, workItem.State, workItem.StateUpdated);
    
    }
    IReadOnlyCollection<WorkItemDTO> Read();
    IReadOnlyCollection<WorkItemDTO> ReadRemoved();
    IReadOnlyCollection<WorkItemDTO> ReadByTag(string tag);
    IReadOnlyCollection<WorkItemDTO> ReadByUser(int userId);
    IReadOnlyCollection<WorkItemDTO> ReadByState(State state);
    Response Update(WorkItemUpdateDTO workItem);
    Response Delete(int workItemId);

}
