namespace Assignment.Infrastructure.Tests;
using Assignment.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Assignment.Core;
using Microsoft.Data.Sqlite;

public class WorkItemRepositoryTests : IDisposable
{
    
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
        var actual = _repo.Create(new WorkItemCreateDTO("Make Food",null,"Go make a non-pasta dish for three people",new List<string>()));
        // When
        var expectedResponse = Response.Created;
        // Then
        Assert.Equal(expectedResponse, actual.Response);
    }

    [Fact]
    public void WorkItem_with_State_New_Should_be_deleteable()
    {
         // Given
        var testman = _repo.Create(new WorkItemCreateDTO("Make Food",null,"Go make a non-pasta dish for three people",new List<string>(){"#Food"}));
        // When
        _repo.Delete(testman.WorkItemId);
        
        // Then
        _context.WorkItems.Find(testman.WorkItemId).Should().Be(null);
        
    }

    [Fact]
    public void WorkItem_with_State_Active_should_be_state_removed_when_deleted()
    {
         // Given
        var testman = _repo.Create(new WorkItemCreateDTO("Make Food",null,"Go make a non-pasta dish for three people",new List<string>(){"#Food"}));
        var testmanDetails = _context.WorkItems.Find(testman.WorkItemId);
        _repo.Update(new WorkItemUpdateDTO(testmanDetails.Id, testmanDetails.Title, null, testmanDetails.Description, null, State.Active));
        // When
        _repo.Delete(testman.WorkItemId);
        testmanDetails = _context.WorkItems.Find(testman.WorkItemId);
        // Then
        testmanDetails.State.Should().Be(State.Removed);
        
    }

     [Fact]
    public void Deleting_WorkItem_with_State_Resolved_Should_return_response_conflict()
    {
         // Given
        var testman = _repo.Create(new WorkItemCreateDTO("Make Food",null,"Go make a non-pasta dish for three people",new List<string>(){"#Food"}));
        var testmanDetails = _context.WorkItems.Find(testman.WorkItemId);
        _repo.Update(new WorkItemUpdateDTO(testmanDetails.Id, testmanDetails.Title, null, testmanDetails.Description, null, State.Resolved));
        // When
        Response response =_repo.Delete(testman.WorkItemId);
        // Then
        response.Should().Be(Response.Conflict);
        
    }

     [Fact]
    public void Deleting_WorkItem_with_State_Closed_Should_return_response_conflict()
    {
         // Given
        var testman = _repo.Create(new WorkItemCreateDTO("Make Food",null,"Go make a non-pasta dish for three people",new List<string>(){"#Food"}));
        var testmanDetails = _context.WorkItems.Find(testman.WorkItemId);
        _repo.Update(new WorkItemUpdateDTO(testmanDetails.Id, testmanDetails.Title, null, testmanDetails.Description, null, State.Closed));
        // When
        Response response =_repo.Delete(testman.WorkItemId);
        // Then
        response.Should().Be(Response.Conflict);
        
    }

     [Fact]
    public void Deleting_WorkItem_with_State_Removed_Should_return_response_conflict()
    {
         // Given
        var testman = _repo.Create(new WorkItemCreateDTO("Make Food",null,"Go make a non-pasta dish for three people",new List<string>(){"#Food"}));
        var testmanDetails = _context.WorkItems.Find(testman.WorkItemId);
        _repo.Update(new WorkItemUpdateDTO(testmanDetails.Id, testmanDetails.Title, null, testmanDetails.Description, null, State.Removed));
        // When
        Response response =_repo.Delete(testman.WorkItemId);
        // Then
        response.Should().Be(Response.Conflict);
        
    }

  [Fact]
    public void New_WorkItems_State_Should_be_New()
    {
         // Given
        var testman = _repo.Create(new WorkItemCreateDTO("Make Food",null,"Go make a non-pasta dish for three people",new List<string>(){"#Food"}));
        var testmanDetails = _context.WorkItems.Find(testman.WorkItemId);
        // When
    
        // Then
        testmanDetails.State.Should().Be(State.New);
        
    }
    [Fact]
    public void New_WorkItems_Should_Have_CreatedTime_equal_currentTime()
    {
         // Given
        var testman = _repo.Create(new WorkItemCreateDTO("Make Food",null,"Go make a non-pasta dish for three people",new List<string>(){"#Food"}));
        var testmanDetails = _context.WorkItems.Find(testman.WorkItemId);
        // When
        var testTime =_repo.Find(testman.WorkItemId).Created;
        // Then
       testTime.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(5));
        
    }

     [Fact]
    public void New_WorkItems_Should_Have_StateUpdateTime_equal_currentTime()
    {
         // Given
        var testman = _repo.Create(new WorkItemCreateDTO("Make Food",null,"Go make a non-pasta dish for three people",new List<string>(){"#Food"}));
        var testmanDetails = _context.WorkItems.Find(testman.WorkItemId);
        // When
        var testTime =_repo.Find(testman.WorkItemId).StateUpdated;
        // Then
       testTime.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(5));
        
    }

    [Fact]
    public void Changeing_State_should_change_StateUpdated_to_CurrentTime()
    {
         // Given
        var testman = _repo.Create(new WorkItemCreateDTO("Make Food",null,"Go make a non-pasta dish for three people",new List<string>(){"#Food"}));
        var testmanDetails = _context.WorkItems.Find(testman.WorkItemId);

        // When
        System.Threading.Thread.Sleep(5000);
        _repo.Update(new WorkItemUpdateDTO(testmanDetails.Id, testmanDetails.Title, null, testmanDetails.Description, null, State.Removed));
        var testTime =_repo.Find(testman.WorkItemId).StateUpdated;
        // Then
       testTime.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(3));
        
    }
    

    void IDisposable.Dispose()
    {
        _context.Dispose();
    }
    
}
