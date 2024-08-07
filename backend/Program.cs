using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Rewrite;


var builder = WebApplication.CreateBuilder(args);

// Configure Services
builder.Services.AddSingleton<ITaskService>(new InMemoryTaskService());
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAllOrigins",
        builder => {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});


var app = builder.Build();

// Redirect /tasks to /todos
app.UseRewriter(new RewriteOptions().AddRedirect("tasks/(.*)", "todos/$1"));


// Add Todo Endpoint
app.MapPost("/todos", (Todo task, ITaskService service) => {
    var resultTask = service.AddTodo(task);
    return TypedResults.Created("/todos/{id}", resultTask);
})
.AddEndpointFilter(async(context, next) => {
    var taskArguments = context.GetArgument<Todo>(0);
    var errors = new Dictionary<string, string[]>();
    if (taskArguments.DueDate < DateTime.UtcNow){
        errors.Add(nameof(Todo.DueDate), ["Cannot be in the past"]);
    }
    if (taskArguments.IsCompleted){
        errors.Add(nameof(Todo.IsCompleted), ["Cannot be completed at creation"]);
    }
    if (errors.Count > 0){
        return Results.ValidationProblem(errors);
    }
    return await next(context);
});

// Get All Todos Endpoint
app.MapGet("/todos", (ITaskService service) => service.GetTodos());

// Get Todo by Id Endpoint
app.MapGet("/todos/{id}", Results<Ok<Todo>, NotFound>(int id,ITaskService service) => {
    var targetTodo = service.GetTodoId(id);
    return targetTodo is null ? TypedResults.NotFound() : TypedResults.Ok(targetTodo);
});


// Delete Todo by Id Endpoint
app.MapDelete("/todos/{id}", (int id, ITaskService service) => {
    service.DeleteTodoById(id);
    return TypedResults.NoContent();
});



app.Run();
