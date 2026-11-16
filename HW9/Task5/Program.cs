using System.Text;
using System.Text.Json;

namespace Task5;

internal class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        GameCharacter character = new GameCharacter
        {
            Name = "Arthur",
            Level = 12,
            Health = 87.5,
            Inventory = new[] { "Sword", "Shield", "Health potion" }
        };

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(character, options);
        Console.WriteLine(json);

        string path = Path.Combine(AppContext.BaseDirectory, "save.json");
        File.WriteAllText(path, json);

        string savedJson = File.ReadAllText(path);
        GameCharacter? loaded = JsonSerializer.Deserialize<GameCharacter>(savedJson);
        if (loaded == null)
        {
            Console.WriteLine("Не удалось загрузить персонажа.");
            return;
        }

        Console.WriteLine("=== Загруженный персонаж ===");
        Console.WriteLine($"Имя: {loaded.Name}");
        Console.WriteLine($"Уровень: {loaded.Level}");
        Console.WriteLine($"Здоровье: {loaded.Health}");
        Console.WriteLine($"Инвентарь: {string.Join(", ", loaded.Inventory)}");
    }
}
