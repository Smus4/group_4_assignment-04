namespace Assignment3.Entities.Tests;

public class UserRepositoryTests
{
    private readonly KanbanContext _context;
    private readonly UserRepository _repository;

public UserRepositoryTests () {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<KanbanContext>();
        builder.UseSqlite(connection);
        var context = new KanbanContext(builder.Options);
        context.Database.EnsureCreated();
        _context = context;
        //_repository = new TagRepository(_context);
        //_taskRepository = new TaskRepository(_context);
        _repository = new UserRepository(_context);
    }
   
    [Fact]
    public void Create_User_returns_Created()
    {
        var (response, created) = _repository.Create(new UserCreateDTO("A User","an email"));
       
        response.Should().Be(Response.Created);
       
        created.Should().Be(0);
    }

    [Fact]
    public void Create_User_returns_Conflict()
    {
        var (response, created) = _repository.Create(new UserCreateDTO("A User","an email"));
        var (response1, created1) = _repository.Create(new UserCreateDTO("A User","an email"));
       
        response1.Should().Be(Response.Conflict);
    
    }

    [Fact]
    public void Deleter_User_returns_NotFound()
    {
        var response = _repository.Delete(0, true);
        response.Should().Be(Response.NotFound);
    
    }

    [Fact]
    public void Deleter_User_returns_Conflict()
    {
        var (response, created) = _repository.Create(new UserCreateDTO("A User","an email"));
        response = _repository.Delete(0, false);
        response.Should().Be(Response.Conflict);
    
    }

    [Fact]
    public void Deleter_User_returns_Deleted()
    {
        var (response, created) = _repository.Create(new UserCreateDTO("A User","an email"));
        response = _repository.Delete(0, true);
        response.Should().Be(Response.Deleted);
    
    }


    [Fact]
    public void Read_User_returns_UserDTO()
    {
        _repository.Create(new UserCreateDTO("A User","an email"));
       
        var response = _repository.Read(0);
        var result = new UserDTO(0,"A User", "an email");

        response.Should().Be(result);
    }

    [Fact]
    public void Update_User_returns_NotFound()
    {
        var response = _repository.Update( new UserUpdateDTO(0, "name ", "an email"));
        response.Should().Be(Response.NotFound);
    
    }

    [Fact]
    public void Update_User_returns_Updated()
    {
        var (response, created) = _repository.Create(new UserCreateDTO("A User","an email"));
        var result = _repository.Update( new UserUpdateDTO(0, "A User ", "an email"));
        result.Should().Be(Response.Updated);
    
    }
}
