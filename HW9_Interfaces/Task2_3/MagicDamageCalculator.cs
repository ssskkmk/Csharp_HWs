namespace Task2_3;

public class MagicDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = Math.Max(1, baseDamage - targetArmor / 4);
        if (targetArmor == 0)
        {
            damage *= 2;
        }

        return damage;
    }
}
