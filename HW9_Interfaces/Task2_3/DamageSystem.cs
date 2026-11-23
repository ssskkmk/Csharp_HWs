namespace Task2_3;

public class DamageSystem
{
    private readonly IDamageCalculator calculator;

    public DamageSystem(IDamageCalculator calculator)
    {
        this.calculator = calculator;
    }

    public void ApplyDamage(Unit target, int baseDamage)
    {
        int damage = calculator.CalculateDamage(baseDamage, target.Armor);
        target.HP = Math.Max(0, target.HP - damage);
        Console.WriteLine($"Юнит {target.Name} получил {damage} урона. Осталось HP: {target.HP}");
    }
}
