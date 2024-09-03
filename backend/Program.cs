using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Rewrite;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseUrls("http://0.0.0.0:5000");

// Configure Services
builder.Services.AddSingleton<ITaskService, InMemoryTaskService>();
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAllOrigins",
        builder => {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/health", () => "OK")
.WithName("Health Check")
.WithOpenApi();


app.MapPost("/task", (TaskDTO task, ITaskService service) => {
    var resultTask = service.AddTask(task);
    return TypedResults.Created("/task/{id}", resultTask);
})
.AddEndpointFilter(async(context, next) => {
    var taskArguments = context.GetArgument<TaskDTO>(0);
    var errors = new Dictionary<string, string[]>();
    if (taskArguments.IsCompleted){
        errors.Add(nameof(TaskDTO.IsCompleted), ["Cannot be completed at creation"]);
    }
    if (errors.Count > 0){
        return Results.ValidationProblem(errors);
    }
    return await next(context);
})
.WithName("Add Task")
.WithOpenApi();

app.MapGet("/task", (ITaskService service) => service.GetTasks())
.WithName("Get All Tasks")
.WithOpenApi();

app.MapGet("/task/{id}", Results<Ok<TaskDTO>, NotFound>(int id,ITaskService service) => {
    var targetTask = service.GetTaskId(id);
    return targetTask is null ? TypedResults.NotFound() : TypedResults.Ok(targetTask);
})
.WithName("Get Task by Id")
.WithOpenApi();


app.MapDelete("/task/{id}", (int id, ITaskService service) => {
    service.DeleteTaskById(id);
    return TypedResults.NoContent();
})
.WithName("Delete Task by Id")
.WithOpenApi();


app.Run();
