namespace Task2_1;

public class Mage : ICombatant
{
    public string Name { get; }
    public int HP { get; set; } = 80;
    public bool IsDead => HP <= 0;
    public int Damage { get; } = 40;

    public Mage(string name)
    {
        Name = name;
    }

    public void Attack(ICombatant target)
    {
        if (IsDead)
        {
            Console.WriteLine($"Маг {Name} мёртв и не может атаковать");
            return;
        }

        if (target.IsDead)
        {
            Console.WriteLine($"Маг {Name} не может атаковать мёртвую цель");
            return;
        }

        Console.WriteLine($"Маг {Name} применяет двойное заклинание!");
        target.TakeDamage(Damage);

        if (!target.IsDead)
        {
            target.TakeDamage(Damage);
        }
    }

    public void TakeDamage(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        HP = Math.Max(0, HP - amount);
        Console.WriteLine($"Маг {Name} получил {amount} урона. Осталось HP: {HP}");
    }
}
