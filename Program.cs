using MinimalAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/Hello", () =>
{
    return "Hello World!!";
});

List<Employee> employees = new List<Employee>();

app.MapGet("/employee", () =>
{
    return employees;
});

app.MapPost("/employee", (Employee employee) =>  
{
    employees.Add(employee);
    return Results.Ok(employee);
});

app.MapPut("/employee/{id}", (int id, Employee employee) =>
{
    int index = employees.FindIndex(employee => employee.Id == id);
    if (index == -1)
        return Results.NotFound();

    employees[index] = employee;
    return Results.Ok();
});

app.MapDelete("/employee/{id}", (int id) =>
{
   int count = employees.RemoveAll(employee => employee.Id == id);
    if (count > 0)
    return Results.Ok();
    return Results.NotFound();
});


app.Run();

