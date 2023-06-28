namespace Interfaces
{
    public interface IAttacker
    {
        void Attack(IDamageable target);
        float Damage { get; }
    }
}