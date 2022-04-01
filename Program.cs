// начальные данные
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
    // получаем по id
    Registered? user = users.FirstOrDefault(u => u.Id == id);
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "User not found!" });

    // если найден, отправляем его
    return Results.Json(user);
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