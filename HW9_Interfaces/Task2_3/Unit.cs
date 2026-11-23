namespace Task2_3;

public class Unit
{
    public int HP { get; set; }
    public int Armor { get; set; }
    public string Name { get; }

    public Unit(string name, int hp, int armor)
    {
        Name = name;
        HP = hp;
        Armor = armor;
    }
}
