using Microsoft.EntityFrameworkCore;


public class Task{
    public int Id {get; set; }
    public string Name {get; set; }
    public DateTime DueDate {get; set; }
    public bool IsCompleted {get; set; }
}

public class TaskContext : DbContext{
    public DbSet<Task> Tasks {get; set; }
    public string DBPath { get; } = "task.db";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        optionsBuilder.UseSqlite($"Data Source={DBPath}");
    }
}   
