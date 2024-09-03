using AutoMapper;


public class Task{
    public int Id { get; set; }
    public required string Name { get; set; }
    public bool IsCompleted { get; set; }
}

public class TaskDTO{
    public int Id { get; init; }
    public required string Name { get; init; }
    public bool IsCompleted { get; init; }
}


public class TaskMapper: Profile{
    public TaskMapper(){
        CreateMap<TaskDTO, Task>();
        CreateMap<Task, TaskDTO>();
    }
}
