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
        throw new NotImplementedException();
    }

    public Response Delete(int workItemId)
    {
        throw new NotImplementedException();
    }

    public WorkItemDetailsDTO Find(int workItemId)
    {
        throw new NotImplementedException();
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
                select new WorkItemDTO(w.Id, w.Title, w.AssignedTo.Name, w.Tags, w.State);

        return workItems.ToList();
    }

    public IReadOnlyCollection<WorkItemDTO> ReadByTag(string tag)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<WorkItemDTO> ReadByUser(int userId)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<WorkItemDTO> ReadRemoved()
    {
        throw new NotImplementedException();
    }

    public Response Update(WorkItemUpdateDTO workItem)
    {
        throw new NotImplementedException();
    }
}
