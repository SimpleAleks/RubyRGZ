var builder = WebApplication.CreateBuilder(args);

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

var balance = 0.0;

app.MapGet("/deposit", (double value) =>
{
    if (balance < 0) return Results.BadRequest();
    if (value < 0) return Results.BadRequest();
    balance += value;
    return Results.Ok(balance);
});

app.MapGet("/withdraw", (double value) =>
{
    if (balance < 0) return Results.BadRequest();
    if (value < 0) return Results.BadRequest();
    if (value > balance) return Results.BadRequest();
    balance -= value;
    return Results.Ok(balance);
});

app.MapGet("/balance", () => Results.Ok(balance));

app.Run();