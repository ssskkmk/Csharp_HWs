namespace Task2_3;

public class FireDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = Math.Max(1, baseDamage - targetArmor / 10);
        if (baseDamage > 50)
        {
            damage += 10;
        }

        return damage;
    }
}
