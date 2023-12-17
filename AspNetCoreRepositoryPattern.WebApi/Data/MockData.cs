using AspNetCoreRepositoryPattern.Models.Dtos;
using AspNetCoreRepositoryPattern.Models.Entities;

namespace AspNetCoreRepositoryPattern.Data;

public static class MockData
{
    public static IEnumerable<TodoDto> GetAllTodos()
    {
        var todoDtos = new List<TodoDto>
        {
            new()
            {
                Id= new Guid("117366b8-3541-4ac5-8732-860d698e26a2"),
                Name="Eat",
                Done = false
            },
            new()
            {
                Id= new Guid("66ff5116-bcaa-4061-85b2-6f58fbb6db25"),
                Name="Sleep",
                Done = false
            },
            new()
            {
                Id =  new Guid("cd5089dd-9754-4ed2-b44c-488f533243ef"),
                Name="Shop",
                Done = true
            },
            new()
            {
                Id =  new Guid("d81e0829-55fa-4c37-b62f-f578c692af78"),
                Name="Code",
                Done = false
            }
        };

        return todoDtos;
    }
    public static Todo GetOneTodo()
    {
        var todo = new Todo {  Id= new Guid("45612a92-bab4-4d35-97c8-55f69e49745c"),
            Name = "Booking",
            Done = false
        };
            
        return todo;
    }
        
    public static TodoDto GetOneTodoDto()
    {
        var todoDto = new TodoDto {  Id= new Guid("45612a92-bab4-4d35-97c8-55f69e49745c"),
            Name = "Booking",
            Done = false
        };
            
        return todoDto;
    }
}