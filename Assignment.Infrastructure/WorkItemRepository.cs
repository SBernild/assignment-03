namespace Assignment.Infrastructure;
using System.Collections.Generic;
using Assignment.Core;

public class WorkItemRepository : IWorkItemRepository
{
    private readonly KabanContext _context;

    public WorkItemRepository(KabanContext context){
        _context = context;
    }
    public (Response Response, int WorkItemId) Create(WorkItemCreateDTO workItem)
    {
        var entity = new WorkItem{
            Title = workItem.Title
        };

        var exists = from w in _context.WorkItems
        where w.Title == w.Title
        select new WorkItemDTO(w.Id, w.Title, w.AssignedTo.Name, w.Tags, w.State);

        if (exists.Any()){
            return (Response.Conflict, -1);
        }

        _context.WorkItems.Add(entity);
        _context.SaveChanges();

        return (Response.Created, entity.Id);
    }

    public Response Delete(int workItemId)
    {
        throw new NotImplementedException();
    }

    public WorkItemDetailsDTO? Find(int workItemId)
    {
        var workItem = _context.WorkItems.Find(workItemId);

        if(workItem == null){
            return null;
        }

        return new WorkItemDetailsDTO(workItem.Id, workItem.Title, workItem.Description, DateTime.UtcNow, workItem.AssignedTo.Name, workItem.Tags, workItem.State, DateTime.UtcNow);
    }

    public IReadOnlyCollection<WorkItemDTO> Read()
    {
        var workItems = from w in _context.WorkItems
                select new WorkItemDTO(w.Id, w.Title, w.AssignedTo.Name, w.Tags, w.State);

        return workItems.ToList();
    }

    public IReadOnlyCollection<WorkItemDTO> ReadByState(Core.State state)
    {
        var workItems = from w in _context.WorkItems
                where w.State == state
                select new WorkItemDTO(w.Id, w.Title, w.AssignedTo.Name, w.Tags, w.State);

        return workItems.ToList();
    }

    public IReadOnlyCollection<WorkItemDTO> ReadByTag(string tag)
    {
        var workItems = from w in _context.WorkItems
                where w.Tags.Contains(tag)
                select new WorkItemDTO(w.Id, w.Title, w.AssignedTo.Name, w.Tags, w.State);

        return workItems.ToList();
    }

    public IReadOnlyCollection<WorkItemDTO> ReadByUser(int userId)
    {
        var workItems = from w in _context.WorkItems
                where w.AssignedTo.Id == userId
                select new WorkItemDTO(w.Id, w.Title, w.AssignedTo.Name, w.Tags, w.State);

        return workItems.ToList();
    }

    public IReadOnlyCollection<WorkItemDTO> ReadRemoved()
    {
        var workItems = from w in _context.WorkItems
                where w.State == State.Removed
                select new WorkItemDTO(w.Id, w.Title, w.AssignedTo.Name, w.Tags, w.State);

        return workItems.ToList();
    }

    public Response Update(WorkItemUpdateDTO workItem)
    {
        var entity = _context.WorkItems.Find(workItem.Id);

        if(entity == null){
            return Response.NotFound;
        }

        entity.Title = workItem.Title;

        _context.SaveChanges();

        return Response.Updated;
    }
}
