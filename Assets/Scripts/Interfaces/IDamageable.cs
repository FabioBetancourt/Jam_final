namespace Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(float amount);
        float Health { get; }
    }
}