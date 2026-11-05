namespace Library.Domain.Entities;

public class Author
{
    public int Id { get; set; }
    public int IdBook { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
