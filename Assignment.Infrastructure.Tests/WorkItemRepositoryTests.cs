namespace Assignment.Infrastructure.Tests;
using Assignment.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Assignment.Core;
using Microsoft.Data.Sqlite;

public class WorkItemRepositoryTests : IDisposable
{
    private KabanContext _context;
    public TagRepository _repo;

    public WorkItemRepositoryTests(){
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<KabanContext>();
        builder.UseSqlite(connection);
        var context = new KabanContext(builder.Options);
        context.Database.EnsureCreated();
        context.Tags.Add(new Tag{Id = 1, Name = "frederik"});
        context.SaveChanges();

        _context = context;
        _repo = new TagRepository(_context);
    }

    [Fact]
    public void find_none_existing_tag()
    {
        // Given
        var (response, created) = _repo.Create(new TagCreateDTO("yo"));
        // When
        response.Should().Be(Response.Created);
        created.Should().Be(2);
        // Then
    }

    void IDisposable.Dispose()
    {
        _context.Dispose();
    }

}
