namespace Assignment.Infrastructure.Tests;
using Assignment.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Assignment.Core;
using Microsoft.Data.Sqlite;

public class WorkItemRepositoryTests : IDisposable
{
    /*
    private KabanContext _context;
    public WorkItemRepository _repo;

    public WorkItemRepositoryTests(){
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

    [Fact]
    public void CreateWorkItemTest()
    {
        // Given
        var actual = _repo.Create(new WorkItemCreateDTO("To do", 2, "Thing to do today", new List<string>()));
        // When
        var expectedResponse = Response.Created;
        // Then
        Assert.Equal(expectedResponse, actual.Response);
    }
    

    void IDisposable.Dispose()
    {
        _context.Dispose();
    }
    */
}
