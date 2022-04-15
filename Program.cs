List<Registered> users = new List<Registered>
{
    new() { Id = Guid.NewGuid().ToString(), Name = "Admin", Password = "12345678" },
    new() { Id = Guid.NewGuid().ToString(), Name = "Guest", Password = "" },
};

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", () => users);

app.MapGet("/api/users/{id}", (string id) =>
{
    Registered? user = users.FirstOrDefault(u => u.Id == id);
    if (user == null) return Results.NotFound(new { message = "User not found!" });

    return Results.Json(user);
});

app.MapPost("/api/users", (Registered user) => {

    user.Id = Guid.NewGuid().ToString();
    users.Add(user);
    return user;
});

// регистрация
app.MapPost("/api/users", (Registered user) => {

    // устанавливаем id для нового
    user.Id = Guid.NewGuid().ToString();
    // добавляем в список
    users.Add(user);
    return user;
});

app.Run();

public class Registered
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Password { get; set; } = "";
}
