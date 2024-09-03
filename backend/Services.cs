using AutoMapper;


interface ITaskService{
    TaskDTO? GetTaskId(int id);
    List<TaskDTO> GetTasks();
    void DeleteTaskById(int id);
    TaskDTO AddTask(TaskDTO task);
}


class InMemoryTaskService : ITaskService{
    private readonly List<Task> _tasks = new();
    private readonly IMapper _mapper;

    public InMemoryTaskService(IMapper mapper){
        _mapper = mapper;
    }

  public TaskDTO AddTask(TaskDTO task){
        var newId = _tasks.Count + 1;
        var newTask = _mapper.Map<Task>(task);
        newTask.Id = newId;
        _tasks.Add(newTask);
        return _mapper.Map<TaskDTO>(newTask);
    }

    public void DeleteTaskById(int id){
        _tasks.RemoveAll(t => id == t.Id);
    }

    public TaskDTO? GetTaskId(int id){
        var task = _tasks.SingleOrDefault(t => t.Id == id);
        return task != null ? _mapper.Map<TaskDTO>(task) : null;
    }

    public List<TaskDTO> GetTasks(){
        return _tasks.Select(_mapper.Map<TaskDTO>).ToList();
    }
}


// class SqliteTaskService: ITaskService{
//     private readonly TaskContext _context;
//     public SqliteTaskService(){
//         _context = new TaskContext();
//         _context.Database.EnsureCreated();
//     }


//     public Todo AddTodo(Todo task){
//         var newTask = new Task{
//             Name = task.Name,
//             DueDate = task.DueDate,
//             IsCompleted = task.IsCompleted
//         };
        
//         _context.Tasks.Add(newTask);
//         _context.SaveChanges();
//         return new Todo(newTask.Id, newTask.Name, newTask.DueDate, newTask.IsCompleted);
//     }

//     public void DeleteTodoById(int id){
//         var task = _context.Tasks.Find(id);
//         if (task != null){
//             _context.Tasks.Remove(task);
//             _context.SaveChanges();
//         }
//     }

//     public Todo? GetTodoId(int id){
//         var task = _context.Tasks.Find(id);
//         if (task != null){
//             return new Todo(task.Id, task.Name, task.DueDate, task.IsCompleted);
//         }
//         return null;
//     }

//     public List<Todo> GetTodos(){
//         return [.. _context.Tasks.Select(t => new Todo(t.Id, t.Name, t.DueDate, t.IsCompleted))];
//     }
// }
