using System.Text;

namespace Task2_1;

internal class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        ICombatant knight = new Knight("Артур");
        ICombatant mage = new Mage("Мерлин");

        while (!knight.IsDead && !mage.IsDead)
        {
            knight.Attack(mage);

            if (!mage.IsDead)
            {
                mage.Attack(knight);
            }
        }

        Console.WriteLine(knight.IsDead
            ? $"Победил Маг {mage.Name}!"
            : $"Победил Рыцарь {knight.Name}!");
    }
}
