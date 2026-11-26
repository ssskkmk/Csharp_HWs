namespace Task2_2;

public class Sword : IUsableItem
{
    public string Name => "Меч";
    public string Description => "Увеличивает урон на 10";
    public Rarity Rarity => Rarity.Rare;

    public void Use(Player player)
    {
        player.IncreaseDamage(10);
        Console.WriteLine("Вы взяли меч. Урон увеличен на 10");
    }
}
