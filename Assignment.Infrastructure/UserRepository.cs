namespace Assignment.Infrastructure;
using System.Collections.Generic;
using Assignment.Core;

public class UserRepository : IUserRepository
{
    private readonly KabanContext _context;

    public UserRepository(KabanContext context){
        _context = context;
    }
    (Response Response, int UserId) Create(UserCreateDTO user)
    {
        var entity = new User{
            Name = user.Name
        };

        var exists = from u in _context.Users
        where u.Name == user.Name
        select new UserDTO(u.Id, u.Name, u.Email);

        if (exists.Any()){
            return (Response.Conflict, -1);
        }

        _context.Users.Add(entity);
        _context.SaveChanges();

        return (Response.Created, entity.Id);
    }
    UserDTO Find(int userId)
    {
        var user = _context.Users.Find(userId);

        if(user == null){
            return null;
        }

        return new UserDTO(user.Id, user.Name, user.Email);
    }
    IReadOnlyCollection<UserDTO> Read()
    {
        var users = from u in _context.Users
            select new UserDTO(u.Id, u.Name, u.Email);

        return users.ToList();
    }
    Response Update(UserUpdateDTO user)
    {
        var entity = _context.Users.Find(user.Id);

        if(entity == null){
            return Response.NotFound;
        }

        entity.Name = user.Name;

        _context.SaveChanges();

        return Response.Updated;
    }
    Response Delete(int userId, bool force = false)
    {
        var entity = new User{Id = userId};
        var exists = from u in _context.Users
        where u.Id == userId
        select new UserDTO(u.Id, u.Name, u.Email);
        if (exists.Any()){
            _context.Users.Remove(entity);
            _context.SaveChanges();
            return (Response.Deleted);
        }
        else return Response.NotFound;
    }
}
