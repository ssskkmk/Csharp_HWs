namespace Task2_2;

public class AncientScroll : IUsableItem
{
    public string Name => "Древний свиток";
    public string Description => "Отнимает 20 HP, но увеличивает урон на 30";
    public Rarity Rarity => Rarity.Legendary;

    public void Use(Player player)
    {
        player.HP = Math.Max(0, player.HP - 20);
        player.IncreaseDamage(30);
        Console.WriteLine("Проклятие свитка нанесло 20 урона. Ваш урон увеличен на 30");
    }
}
