namespace Assignment.Infrastructure;
using System.Collections.Generic;
using Assignment.Core;

public class TagRepository : ITagRepository
{
    private readonly KabanContext _context;

    public TagRepository(KabanContext context){
        _context = context;
    }

    (Response response, int TagId) Create(TagCreateDTO tag){
        var entity = new Tag{
            Name = tag.Name
        };

        var exists = from t in _context.Tags
        where t.Name == tag.Name
        select new TagDTO(t.Id, t.Name);

        if (exists.Any()){
            return (Response.Conflict, -1);
        }

        _context.Tags.Add(entity);
        _context.SaveChanges();

        return (Response.Created, entity.Id);
    }

    Response ITagRepository.Delete(int tagId, bool force)
    {
        var entity = new Tag{Id = tagId};
        var exists = from t in _context.Tags
        where t.Id == tagId
        select new TagDTO(t.Id, t.Name);
        if (exists.Any()){
            _context.Tags.Remove(entity);
            _context.SaveChanges();
            return (Response.Deleted);
        }
        else return Response.NotFound;
    }

    TagDTO ITagRepository.Find(int tagId)
    {
        var tag = _context.Tags.Find(tagId);

        if(tag == null){
            return null;
        }

        return new TagDTO(tag.Id, tag.Name);
    }

    IReadOnlyCollection<TagDTO> ITagRepository.Read()
    {
        var tags = from t in _context.Tags
            select new TagDTO(t.Id, t.Name);

        return tags.ToList();
    }


    Response ITagRepository.Update(TagUpdateDTO tag)
    {
        var entity = _context.Tags.Find(tag.Id);

        if(entity == null){
            return Response.NotFound;
        }

        entity.Name = tag.Name;

        _context.SaveChanges();

        return Response.Updated;
    }
}
