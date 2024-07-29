


interface ITaskService{
    Todo? GetTodoId(int id);
    List<Todo> GetTodos();
    void DeleteTodoById(int id);
    Todo AddTodo(Todo task);
}


class InMemoryTaskService: ITaskService{
    private readonly List<Todo> _todos = [];

    public Todo AddTodo(Todo task){
        _todos.Add(task);
        return task;
    }

    public void DeleteTodoById(int id){
        _todos.RemoveAll(t => id == t.Id);
    }

    public Todo? GetTodoId(int id){
        return _todos.SingleOrDefault(t => id == t.Id);
    }

    public List<Todo> GetTodos(){
        return _todos;
    }
}


class SqliteTaskService: ITaskService{
    private readonly TaskContext _context;
    public SqliteTaskService(){
        _context = new TaskContext();
        _context.Database.EnsureCreated();
    }


    public Todo AddTodo(Todo task){
        var newTask = new Task{
            Name = task.Name,
            DueDate = task.DueDate,
            IsCompleted = task.IsCompleted
        };
        
        _context.Tasks.Add(newTask);
        _context.SaveChanges();
        return new Todo(newTask.Id, newTask.Name, newTask.DueDate, newTask.IsCompleted);
    }

    public void DeleteTodoById(int id){
        var task = _context.Tasks.Find(id);
        if (task != null){
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }

    public Todo? GetTodoId(int id){
        var task = _context.Tasks.Find(id);
        if (task != null){
            return new Todo(task.Id, task.Name, task.DueDate, task.IsCompleted);
        }
        return null;
    }

    public List<Todo> GetTodos(){
        return [.. _context.Tasks.Select(t => new Todo(t.Id, t.Name, t.DueDate, t.IsCompleted))];
    }
}
