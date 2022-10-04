namespace Assignment3.Entities;
#pragma warning disable

public class Tag
{
   
    public int Id {get; set;}

    [Required, Key, StringLength(50)]
    public string ?Name {get; set;}
    public virtual ICollection<Task> Tasks { get; set; }
}
