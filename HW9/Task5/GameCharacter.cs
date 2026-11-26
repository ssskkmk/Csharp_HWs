namespace Task5;

public class GameCharacter
{
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public double Health { get; set; }
    public string[] Inventory { get; set; } = Array.Empty<string>();
}
