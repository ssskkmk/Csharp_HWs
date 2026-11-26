using System.Text;

namespace Task2_2;

internal class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Player player = new Player("Ростислав", 100, 20);
        List<IUsableItem> items = new List<IUsableItem>
        {
            new HealthPotion(),
            new Sword(),
            new AncientScroll()
        };

        foreach (IUsableItem item in items)
        {
            Console.WriteLine($"{item.Name} ({item.Rarity}): {item.Description}");
            item.Use(player);
        }

        Console.WriteLine();
        Console.WriteLine($"Игрок: {player.Name}");
        Console.WriteLine($"HP: {player.HP}");
        Console.WriteLine($"Урон: {player.Damage}");
        Console.WriteLine($"Состояние: {(player.HP > 0 ? "жив" : "мёртв")}");
    }
}
