using System.ComponentModel.DataAnnotations;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

List<BubleGum> repo= [];

app.MapGet("/", () => repo);
app.MapPost("/", (BubleGum s) => repo.Add(s));
app.MapPut("/{id}", (BubleGumUp s, string id) => 
{
    BubleGum buffer = repo.Find(s => s.Name == id);
    if (buffer != null)
        return Results.NotFound("Не найдено");
        buffer.Name = s.name;
    if (buffer != null)
        buffer.Price = s.price;
    if (buffer != null)
        buffer.Life = s.life;
    if (buffer != null)
        buffer.Tastefully = s.tastefully;
    return Results.Json(buffer);
});
app.MapDelete("/delete/{id}", (string id) => 
{
    BubleGum Delete = repo.Find(s => s.Name == id);
    if (Delete != null)
    {
        repo.Remove(Delete);
        return Results.NotFound();
    }
    return Results.NotFound();
});

app.Run();

class BubleGum
{
    [Required]
    [Range(1, 20)]
    public string Name { get; set; }

    [Required]
    [Range(1, 100)]
    public int Price { get; set; }

    [Required]
    [Range(typeof(DateTime), "2025-01-01", "2999-12-31")]
    public DateTime Life { get; set; }

    [Required]
    [Range(1, 50)]
    public string Tastefully { get; set; }
}
record BubleGumUp(string name, int price, DateTime life, string tastefully);