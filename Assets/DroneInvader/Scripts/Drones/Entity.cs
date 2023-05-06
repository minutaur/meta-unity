using UnityEngine;
using UnityEngine.Events;

namespace DroneInvader.Scripts
{
    public class Entity : MonoBehaviour
    {
        public int maxHealth = 100;
        public int curHealth;

        public UnityEvent<Entity> onDamaged, onDeath;

        protected virtual void Start()
        {
            curHealth = maxHealth;
        }

        public virtual void TakeDamage(int damage)
        {
            curHealth -= damage;
            if (curHealth <= 0)
                Die();
        }
        
        public virtual void TakeDamage(int damage, Entity attacker)
        {
            curHealth -= damage;
            onDamaged?.Invoke(attacker);
            if (curHealth <= 0)
                Die(attacker);
        }
    
        public virtual void TakeHeal(int heal)
        {
            curHealth += heal;
            if (curHealth > maxHealth)
                curHealth = maxHealth;
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
        protected virtual void Die(Entity killer)
        {
            PlayerHUDManager.Instance.ShowKillLog(this, killer);
            onDeath?.Invoke(killer);
            Destroy(gameObject);
        }
    }
}