using System.Text;
using System.Text.Json;

namespace Task4;

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

        GameCharacter? restored = JsonSerializer.Deserialize<GameCharacter>(json);
        if (restored == null)
        {
            Console.WriteLine("Не удалось восстановить персонажа.");
            return;
        }

        Console.WriteLine($"Имя: {restored.Name}");
        Console.WriteLine($"Количество предметов: {restored.Inventory.Length}");
    }
}
