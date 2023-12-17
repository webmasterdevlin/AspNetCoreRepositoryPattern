namespace AspNetCoreRepositoryPattern.Models.Dtos;

public class TodoDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Done { get; set; }
}