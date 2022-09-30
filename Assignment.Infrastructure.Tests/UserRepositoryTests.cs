namespace Assignment.Infrastructure.Tests;
using Assignment.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Assignment.Core;
using Microsoft.Data.Sqlite;

public class UserRepositoryTests : IDisposable
{
    private KabanContext _context;
    public WorkItemRepository _repo;

    public UserRepositoryTests(){
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<KabanContext>();
        builder.UseSqlite(connection);
        var context = new KabanContext(builder.Options);
        context.Database.EnsureCreated();
        context.SaveChanges();

        _context = context;
        _repo = new WorkItemRepository(_context);
    }

    /*[Fact]
    public void CreateUserTest()
    {
        // Given
        var actual = _repo.Create(new UserDTO();
        // When
        var expectedResponse = Response.Created;
        // Then
        Assert.Equal(expectedResponse, actual.Response);
    }*/




    void IDisposable.Dispose()
    {
        _context.Dispose();
    }


}
