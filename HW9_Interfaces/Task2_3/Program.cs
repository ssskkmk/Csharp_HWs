using System.Text;

namespace Task2_3;

internal class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Unit target = new Unit("Голем", 200, 40);
        IDamageCalculator[] calculators =
        {
            new PhysicalDamageCalculator(),
            new MagicDamageCalculator(),
            new FireDamageCalculator()
        };

        foreach (IDamageCalculator calculator in calculators)
        {
            target.HP = 200;
            Console.WriteLine(calculator.GetType().Name);
            DamageSystem system = new DamageSystem(calculator);
            system.ApplyDamage(target, 60);
        }

        Console.WriteLine("Без брони:");
        target.Armor = 0;

        foreach (IDamageCalculator calculator in calculators)
        {
            target.HP = 200;
            Console.WriteLine(calculator.GetType().Name);
            DamageSystem system = new DamageSystem(calculator);
            system.ApplyDamage(target, 60);
        }
    }
}
