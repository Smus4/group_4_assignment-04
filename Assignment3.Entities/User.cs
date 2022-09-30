namespace Assignment3.Entities;
#pragma warning disable
public class User
{
    public int Id {get; set;}

    [Required, StringLength(100)]
    public string Name{get; set;}

    [Required, Key, StringLength(100)]
    public string Email{get; set;}

    public virtual ICollection<Task> Tasks {get; set;}
}
