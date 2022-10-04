using Assignment3.Core;

namespace Assignment3.Entities;

public class UserRepository : IUserRepository{

    private readonly KanbanContext _context;

    public UserRepository(KanbanContext context){
        _context = context;
    }

    public (Response Response, int UserId) Create(UserCreateDTO user)
    {
        var entity = _context.Users.FirstOrDefault(u => u.Email == u.Email);
        Response response;

        if (entity is null){
            entity = new User(){
                Name = user.Name,
                Email = user.Email
            };
                        
            
            _context.Users.Add(entity);
            _context.SaveChanges();

            response = Response.Created;
        } else {
            response = Response.Conflict;
        }
        return (response, entity.Id);

    }

    public Response Delete(int userId, bool force = false)
    {
        var entity = _context.Users.FirstOrDefault(u => u.Id == userId);

        if (entity == null){
            return Response.NotFound;
        }
        if(!force){
            return Response.Conflict;
        }

        _context.Users.Remove(entity);
        _context.SaveChanges();

        return Response.Deleted;
    }

    public UserDTO Read(int userId)
    {
        var users = from u in _context.Users
                    where u.Id == userId
                    select new UserDTO(u.Id, u.Name, u.Email);
        
        return users.FirstOrDefault();
    }

    public IReadOnlyCollection<UserDTO> ReadAll()
    {
        var users = from u in _context.Users
                    select new UserDTO(
                        u.Id,
                        u.Name,
                        u.Email
                    );

        return users.ToArray();
    }

    public Response Update(UserUpdateDTO user)
    {
        var entity = _context.Users.FirstOrDefault(u => u.Id == user.Id);

        if (entity == null)
        {
            return Response.NotFound;
        }

            entity.Id = user.Id;
            entity.Email = user.Email;
            entity.Name = user.Name;


            _context.SaveChanges();

            return Response.Updated;
    }
}
