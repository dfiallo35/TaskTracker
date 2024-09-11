using AutoMapper;


interface ITaskService{
    TaskDTO? GetTaskId(int id);
    List<TaskDTO> GetTasks();
    void DeleteTaskById(int id);
    TaskDTO AddTask(TaskDTO task);
}


class InMemoryTaskService : ITaskService{
    private readonly List<Task> _tasks = new();
    private readonly IMapper mapper;

    public InMemoryTaskService(IMapper mapper){
        this.mapper = mapper;
    }

  public TaskDTO AddTask(TaskDTO task){
        var newId = _tasks.Count + 1;
        var newTask = mapper.Map<Task>(task);
        newTask.Id = newId;
        _tasks.Add(newTask);
        return mapper.Map<TaskDTO>(newTask);
    }

    public void DeleteTaskById(int id){
        _tasks.RemoveAll(t => id == t.Id);
    }

    public TaskDTO? GetTaskId(int id){
        var task = _tasks.SingleOrDefault(t => t.Id == id);
        return task != null ? mapper.Map<TaskDTO>(task) : null;
    }

    public List<TaskDTO> GetTasks(){
        return _tasks.Select(mapper.Map<TaskDTO>).ToList();
    }
}


class SqliteTaskService: ITaskService{
    private readonly TaskContext context;
    private readonly IMapper mapper;
    public SqliteTaskService(IMapper mapper){
        context = new TaskContext();
        context.Database.EnsureCreated();
        this.mapper = mapper;
    }


    public TaskDTO AddTask(TaskDTO task){
        var newTask = mapper.Map<Task>(task);
        context.Tasks.Add(newTask);
        context.SaveChanges();
        return mapper.Map<TaskDTO>(newTask);
    }

    public void DeleteTaskById(int id){
        var task = context.Tasks.Find(id);
        if (task != null){
            context.Tasks.Remove(task);
            context.SaveChanges();
        }
    }

    public TaskDTO? GetTaskId(int id){
        var task = context.Tasks.Find(id);
        if (task != null){
            return mapper.Map<TaskDTO>(task);
        }
        return null;
    }

    public List<TaskDTO> GetTasks(){
        return [.. context.Tasks.Select(t => mapper.Map<TaskDTO>(t))];
    }
}
