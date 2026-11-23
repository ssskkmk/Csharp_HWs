namespace Task2_1;

public class Knight : ICombatant
{
    public string Name { get; }
    public int HP { get; set; } = 150;
    public bool IsDead => HP <= 0;
    public int Damage { get; } = 25;

    public Knight(string name)
    {
        Name = name;
    }

    public void Attack(ICombatant target)
    {
        if (IsDead)
        {
            Console.WriteLine($"Рыцарь {Name} мёртв и не может атаковать");
            return;
        }

        if (target.IsDead)
        {
            Console.WriteLine($"Рыцарь {Name} не может атаковать мёртвую цель");
            return;
        }

        Console.WriteLine($"Рыцарь {Name} атакует!");
        target.TakeDamage(Damage);
    }

    public void TakeDamage(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        HP = Math.Max(0, HP - amount);
        Console.WriteLine($"Рыцарь {Name} получил {amount} урона. Осталось HP: {HP}");
    }
}
