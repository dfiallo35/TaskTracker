using Microsoft.EntityFrameworkCore;


public class TaskContext : DbContext{
    public DbSet<Task> Tasks {get; set; }
    public string DBPath { get; } = "task.db";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        optionsBuilder.UseSqlite($"Data Source={DBPath}");
    }
}   
